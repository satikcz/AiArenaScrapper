namespace AIArenaScrapper.Payloads
{
    public class User
    {
        public string username { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public bool is_staff { get; set; }
        public bool is_active { get; set; }
        public DateTime date_joined { get; set; }
        public string patreon_level { get; set; }
        public string type { get; set; }
        public int id { get; set; }

        public override string ToString()
        {
            return $"{id}{(is_staff ? "*" : "")} {first_name} '{username}' {last_name} {(is_active ? "" : "(inactive)")} {patreon_level}, joined {date_joined}";
        }
    }
}
