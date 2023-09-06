using AIArenaScrapper.Payloads;
using AIArenaScrapper.StatsModule;

namespace AIArenaScrapper
{
    /// <summary>
    /// Class for calculating detailed bot statistics
    /// </summary>
    public class DetailedBotStats
    {
        private string _botName;
        private string? _competitionName;
        private ArenaProvider _arenaProvider;
        private int _roundCount;

        Dictionary<int, Map> mapCache = new Dictionary<int, Map>();
        Dictionary<int, Bot> botCache = new Dictionary<int, Bot>();
        Dictionary<string, Bot> botCacheByName = new Dictionary<string, Bot>();

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="arenaProvider">Arena provider that is used to communicate with the AIArena API</param>
        /// <param name="botName">Bot name to calculate the statistics for</param>
        /// <param name="competitionName">Competition name to obtain statistics for. Can be null for automatic competition resolving.</param>
        /// <param name="roundCount">Count of last rounds to obtain the statistics for</param>
        public DetailedBotStats(ArenaProvider arenaProvider, string botName, string? competitionName = null, int roundCount = 5)
        {
            _botName = botName;
            _arenaProvider = arenaProvider;
            _competitionName = competitionName;
            _roundCount = roundCount;
        }

        private async Task<Bot> GetBotAsync(int id)
        {
            if (botCache.TryGetValue(id, out var bot))
            {
                return bot;
            }
            else
            {
                bot = await _arenaProvider.GetBotAsync(id);
                botCache[bot.id] = bot;
                botCacheByName[bot.name]=bot;
                return bot;
            }
        }

        private async Task<Bot> GetBotAsync(string name)
        {
            if (botCacheByName.TryGetValue(name, out var bot))
            {
                return bot;
            }
            else
            {
                bot = await _arenaProvider.GetBotAsync(name);
                botCache[bot.id] = bot;
                botCacheByName[bot.name]=bot;
                return bot;
            }
        }

        private async Task<Map> GetMapAsync(int id)
        {
            if (mapCache.TryGetValue(id, out var map))
            {
                return map;
            }
            else
            {
                map = await _arenaProvider.GetMapAsync(id);
                mapCache[id] = map;
                return map;
            }
        }

        public async Task Run()
        {
            Console.WriteLine($"Obtaining {_botName} details");
            var bot = (await _arenaProvider.GetBotsAsync(new NameFilter(_botName))).FirstOrDefault();

            if (bot is null)
            {
                Console.WriteLine($"{_botName} not found!");
                return;
            }

            SearchFilter? competitionFilter = null;
            if (!string.IsNullOrEmpty(_competitionName))
            {
                Console.WriteLine($"Obtaining competition {_competitionName} details");
                competitionFilter = new NameFilter(_competitionName);
            }
            else
            {
                Console.WriteLine($"Trying to obtain opened competition with interest 5");
                // Try to find opened competition with interest 5
                competitionFilter = new SearchFilter() { { "interest", "5" }, { "status", "open" } };
            }

            // Get competitions
            var competitions = await _arenaProvider.GetCompetitionsAsync(competitionFilter);

            if (!competitions.Any())
            {
                Console.WriteLine($"Competition not found!");
                return;
            }

            Console.WriteLine($"Using competition {competitions.First().name}.");

            // Get rounds belonging to the competition
            Console.WriteLine($"Obtaining {_roundCount} latest competition rounds");
            var roundsFilter = new SearchFilter() { { "competition", competitions.First().id.ToString() } };
            var roundCount = await _arenaProvider.GetRoundCountAsync(roundsFilter);
            var rounds = await _arenaProvider.GetRoundsAsync(roundsFilter + new OrderByFilter("number") + new PagedFilter(Math.Max(0, roundCount - _roundCount), _roundCount));

            // Get matches and match participations for the rounds for given bot
            List<MatchSummary> matches = new();
            foreach (var round in rounds)
            {
                Console.WriteLine($"Obtaining matches for round {round.number} for {_botName}");
                var matchBotFilter = new SearchFilter() { { "bot", $"{bot.id}" } };
                foreach (var match in await _arenaProvider.GetMatchesAsync(matchBotFilter + new SearchFilter() { { "round", $"{round.id}" } }))
                {
                    Console.WriteLine($"   Obtaining match details for match #{match.id}");
                    var participation = (await _arenaProvider.GetMatchParticipationsAsync(new SearchFilter() { { "match", $"{match.id}" } })).FirstOrDefault();
                    var result = (await _arenaProvider.GetResultsAsync(new SearchFilter() { { "match", $"{match.id}" } })).FirstOrDefault();
                    var map = await GetMapAsync(match.map);

                    if (match.result is null)
                    {
                        Console.WriteLine($"   Skipping unfinished match {match.id}");
                        continue;
                    }

                    var enemyBot = await GetBotAsync(match.result.bot1_name.Equals(_botName, StringComparison.InvariantCultureIgnoreCase) ? match.result.bot2_name : match.result.bot1_name);

                    if (participation is not null && result is not null && map is not null && enemyBot is not null)
                    {
                        matches.Add(new MatchSummary(match, result, participation, enemyBot, map));
                    }
                    else
                    {
                        Console.WriteLine($"!! Failed to obtaining match details for match #{match.id} !!!");
                    }
                }
            }

            var stats = new Stats(matches);
            stats.PrintStatsToConsole();
        }
    }
}
