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

        public async Task Run()
        {
            var bot = (await _arenaProvider.GetBotsAsync(new NameFilter(_botName))).FirstOrDefault();

            if (bot is null)
                return;

            SearchFilter? competitionFilter = null;
            if (!string.IsNullOrEmpty(_competitionName))
                competitionFilter = new NameFilter(_competitionName);
            else
                // Try to find opened competition with interest 5
                competitionFilter = new SearchFilter() { { "interest", "5" }, { "status", "open" } };

            // Get competitions
            var competitions = await _arenaProvider.GetCompetitionsAsync(competitionFilter);

            if (!competitions.Any())
                return;

            // Get rounds belonging to the competition
            var roundsFilter = new SearchFilter() { { "competition", competitions.First().id.ToString() } };
            var roundCount = await _arenaProvider.GetRoundCountAsync(roundsFilter);
            var rounds = await _arenaProvider.GetRoundsAsync(roundsFilter + new OrderByFilter("number") + new PagedFilter(Math.Max(0, roundCount - _roundCount), _roundCount));

            // Get matches and match participations for the rounds for given bot
            Dictionary<Match, MatchParticipation> matches = new();
            foreach (var round in rounds)
            {
                var matchBotFilter = new SearchFilter() { { "bot", $"{bot.id}" } };
                foreach (var match in await _arenaProvider.GetMatchesAsync(matchBotFilter + new SearchFilter() { { "round", $"{round.id}" } }))
                {
                    var participations = await _arenaProvider.GetMatchParticipationsAsync(new SearchFilter() { { "match", $"{match.id}" }, { "bot", $"{bot.id}" } });

                    if (participations.Any())
                        matches.Add(match, participations.First());
                }
            }

            // Calculate and print stats
            await PrintStatsAsync(matches);
        }

        private async Task PrintStatsAsync(Dictionary<Match, MatchParticipation> matches)
        {
            // Get maps for printing map names
            var maps = await _arenaProvider.GetMapsAsync();
        }
    }
}
