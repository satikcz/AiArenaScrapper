namespace AIArenaScrapper
{
    public class DetailedBotStats
    {
        private string _botName;
        private string? _competitionName;
        private ArenaProvider _arenaProvider;
        private int _roundCount;

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
                // Try to find latest big competition not closed yet
                competitionFilter = new SearchFilter() { { "interest", "5" }, { "status", "open" } };

            var competitions = await _arenaProvider.GetCompetitionsAsync(competitionFilter);

            //var maps = await _arenaProvider.GetMapsAsync();

            if (!competitions.Any())
                return;

            var roundsFilter = new SearchFilter() { { "competition", competitions.First().id.ToString() } };

            var roundCount = await _arenaProvider.GetRoundCountAsync(roundsFilter);
            var rounds = await _arenaProvider.GetRoundsAsync(roundsFilter + new OrderByFilter("number") + new PagedFilter(Math.Max(0, roundCount - _roundCount), _roundCount));

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
        }
    }
}
