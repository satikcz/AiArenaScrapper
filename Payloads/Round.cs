namespace AIArenaScrapper.Payloads
{
    public class Round
    {
        public int id { get; set; }
        public int number { get; set; }
        public int competition { get; set; }
        public DateTime? started { get; set; }
        public DateTime? finished { get; set; }
        public bool complete { get; set; }
    }
}
