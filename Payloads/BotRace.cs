namespace AIArenaScrapper.Payloads
{
    public class BotRace
    {
        public int id { get; set; }
        public string label { get; set; }

        public override string ToString()
        {
            return $"{label}";
        }
    }
}
