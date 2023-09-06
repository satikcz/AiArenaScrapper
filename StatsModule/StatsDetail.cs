namespace AIArenaScrapper.StatsModule
{
    public class StatsDetail
    {
        public int Wins { get; set; }
        public int Draws { get; set; }
        public int Losses { get; set; }
        public int EloChange { get; set; }
        public List<int> GameLengths { get; private set; } = new List<int>();
        public List<float> FrameDurations { get; private set; } = new List<float>();

        public int MinGameLength => GameLengths.Min();
        public int MaxGameLength => GameLengths.Max();
        public int AvgGameLength => (int)GameLengths.Average();

        public float MinFrameDuration => FrameDurations.Min();
        public float MaxFrameDuration => FrameDurations.Max();
        public float AvgFrameDuration => FrameDurations.Average();

        public int MatchCount => Wins+Draws+Losses;
        public float Winrate => (MatchCount == 0 ? 1.0f : (float)Wins/MatchCount) * 100;

        public void AddMatch(MatchSummary match)
        {
            if (match.Result == Result.Win)
                Wins++;
            else if (match.Result == Result.Loss)
                Losses++;
            else if (match.Result == Result.Draw)
                Draws++;
            else
                return; // Ignore games that didnt finish properly

            EloChange+=match.EloChange;
            GameLengths.Add(match.GameLengthFrames);
            FrameDurations.Add(match.AvgFrameTime);
        }

        public override string ToString()
        {
            return $"{(int)Winrate}%\t{$"{Wins}-{Losses} ({Draws})".PadRight(10)}\tElo: {$"{EloChange:+###;-###;0}".PadRight(8)}\t{AvgFrameDuration:F1}ms\t{TimeSpan.FromSeconds(AvgGameLength / 22.4f):hh\\:mm\\:ss\\.f}";
        }
    }
}
