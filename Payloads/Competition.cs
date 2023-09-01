namespace AIArenaScrapper.Payloads
{
    public class Competition
    {
        public int id { get;set; }
        public string name { get;set; }
        public string type { get;set; }
        public int game_mode { get; set; }
        public DateTime? date_created { get; set; }
        public DateTime? date_opened { get; set; }
        public DateTime? date_closed { get; set; }
        public string status { get; set; }
        public int max_active_rounds { get;set; }
        public int interest { get; set; }
        public int target_n_divisions { get; set; }
        public int n_divisions { get; set; }
        public int target_division_size { get; set; }
        public int rounds_per_cycle { get; set; }
        public int rounds_this_cycle { get; set; }
        public int n_placements { get; set; }

        public override string ToString()
        {
            return $"#{id} {name} ({type}, {status}), {date_opened} to {date_closed}";
        }
    }
}
