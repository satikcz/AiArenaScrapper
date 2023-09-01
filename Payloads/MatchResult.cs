namespace AIArenaScrapper.Payloads
{
    public class MatchResult
    {
        public int id { get; set; }
        public int match { get; set; }
        public int? winner { get; set; }
        public string type { get; set; }
        public DateTime? created { get; set; }
        public string replay_file { get; set; }
        public int game_steps { get; set; }
        public int? submitted_by { get; set; }
        public string arenaclient_log { get; set; }
        public float? interest_rating { get; set; }
        public DateTime? date_interest_rating_calculated { get; set; }
        public bool replay_file_has_been_cleaned { get; set; }
        public bool arenaclient_log_has_been_cleaned { get; set; }
        public string bot1_name { get; set; }
        public string bot2_name { get; set; }

        public override string ToString()
        {
            return $"#{id} winner: {winner} ({bot1_name} vs {bot2_name}) {game_steps} steps, type: {type} interest: {interest_rating}";
        }
    }
}
