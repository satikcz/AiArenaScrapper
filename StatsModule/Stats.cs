namespace AIArenaScrapper.StatsModule
{
    public class Stats
    {
        public readonly Dictionary<string, StatsDetail> BotStats = new Dictionary<string, StatsDetail>();
        public readonly Dictionary<string, StatsDetail> MapStats = new Dictionary<string, StatsDetail>();
        public readonly Dictionary<string, StatsDetail> MatchupStats = new Dictionary<string, StatsDetail>();

        public Stats(IEnumerable<MatchSummary> matches) 
        { 
            foreach (var match in matches)
            {
                AddMatch(BotStats, match.EnemyBot.name, match);
                AddMatch(MapStats, match.Map.name, match);
                AddMatch(MatchupStats, match.EnemyRace, match);

                BotStats[match.EnemyBot.name].AddMatch(match);
                MapStats[match.Map.name].AddMatch(match);
                MatchupStats[match.EnemyRace].AddMatch(match);
            }
        }

        private void AddMatch(Dictionary<string, StatsDetail> dictionary, string key, MatchSummary match)
        {
            if (!dictionary.ContainsKey(key))
            {
                dictionary.Add(key, new StatsDetail());
            }

            dictionary[key].AddMatch(match);
        }

        public void PrintStatsToConsole()
        {
            Console.WriteLine("Stats by bots:");
            Console.WriteLine("");
            foreach (var stat in BotStats) 
            {
                Console.WriteLine($"  {stat.Key.PadRight(20)}\t{stat.Value}");
            }
            Console.WriteLine("");
            
            Console.WriteLine("Stats by maps:");
            Console.WriteLine("");
            foreach (var stat in MapStats)
            {
                Console.WriteLine($"  {stat.Key.PadRight(20)}\t{stat.Value}");
            }
            Console.WriteLine("");

            Console.WriteLine("Stats by matchup:");
            Console.WriteLine("");
            foreach (var stat in MatchupStats)
            {
                Console.WriteLine($"  {stat.Key.PadRight(20)}\t{stat.Value}");
            }
            Console.WriteLine("");
        }
    }
}
