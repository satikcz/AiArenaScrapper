namespace AIArenaScrapper.Payloads
{
    public class MatchParticipation
    {
        public int id { get;set; }
        public int match { get; set; }
        public int participant_number { get; set; }
        public int bot { get; set; }
        public int? starting_elo { get; set; }
        public int? resultant_elo { get; set; }
        public int? elo_change { get; set; }
        public string match_log { get; set; }
        public float? avg_step_time { get; set; }
        public string result { get; set; }
        public string result_cause { get; set; }
        public bool use_bot_data { get; set; }
        public bool update_bot_data { get; set; }
        public bool match_log_has_been_cleaned { get; set; }

        public override string ToString()
        {
            return $"#{id}, match {match}, bot {bot}, elo {elo_change}";
        }
    }
}
