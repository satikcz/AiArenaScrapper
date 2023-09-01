namespace AIArenaScrapper.Payloads
{
    public class Map
    {
        public int id { get; set; }
        public string name { get; set; }
        public string file { get; set; }
        public int game_mode { get; set; }
        public int[] competitions { get; set; }
        public bool enabled {get;set;}
    }
}
