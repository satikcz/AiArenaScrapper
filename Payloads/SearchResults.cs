namespace AIArenaScrapper.Payloads
{
    public class SearchResults<T>
    {
        public int count { get; set; }
        public string next { get; set; }
        public string previous { get;set; }
        public T[] results { get; set; }
    }
}
