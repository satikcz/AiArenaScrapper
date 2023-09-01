namespace AIArenaScrapper.Payloads
{
    public class Match
    {
        public int id { get; set; }
        public int map { get;set; }
        public DateTime? created { get; set; }
        public DateTime? started { get; set; }
        public int? assigned_to { get; set; }
        public int round { get; set; }
        public int? requested_by { get; set; }
        public bool require_trusted_arenaclient { get; set; }
        public MatchResult result { get; set; }
        public MatchTag[] tags { get; set; }

        public override string ToString()
        {
            return $"{id} round: {round} started: {started} {result} tags: {tags}";
        }
    }
}
