namespace AIArenaScrapper.Payloads
{
    public class MatchTag
    {
        public int user { get;set; }
        public string tag_name { get; set; }

        public override string ToString()
        {
            return $"{tag_name}";
        }
    }
}
