namespace AIArenaScrapper.StatsModule
{
    public class MatchSummary
    {
        public MatchSummary(Match match, MatchResult matchResult, MatchParticipation matchParticipation, Bot bot, Map map)
        {
            EnemyBot = bot;
            Map = map;
            MatchResult = matchResult;
            MatchParticipation = matchParticipation;
            Match = match;

            MapName = Map.name;
            EnemyName = EnemyBot.name;
            EloChange = matchParticipation.elo_change ?? 0;
            AvgFrameTime = (matchParticipation.avg_step_time ?? 0) * 1000.0f;
            GameLengthFrames = matchResult.game_steps;
            EnemyRace = bot.plays_race.label;

            Result = matchParticipation.result switch
            {
                "win" => Result.Win,
                "loss" => Result.Loss,
                "draw" => Result.Draw,
                _ => Result.Other,
            };
        }

        public string EnemyName { get; set; }
        public string MapName { get; set; }
        public int EloChange { get; set; }
        public Result Result { get; set; }
        public int GameLengthFrames { get; set; }
        public TimeSpan GameLength => TimeSpan.FromSeconds(GameLengthFrames*22.4f);
        public float AvgFrameTime { get; set; }
        public string EnemyRace { get; set; }

        public Bot EnemyBot { get; set; }
        public Map Map { get; set; }
        public Match Match { get; set; }
        public MatchParticipation MatchParticipation { get; set; }
        public MatchResult MatchResult { get; set; }

        public override string ToString()
        {
            return $"{Result} vs {EnemyName} ({EloChange:+#;-#;0}) on {MapName} ({AvgFrameTime:F1}ms) in {GameLength:hh\\:mm\\:ss\\.f}";
        }
    }
}
