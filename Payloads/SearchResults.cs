namespace AIArenaScrapper.Payloads
{
    /// <summary>
    /// Class for representing search results.
    /// Search results are always returned wrapped in this structure.
    /// </summary>
    public class SearchResults<T>
    {
        public int count { get; set; }
        public string next { get; set; }
        public string previous { get;set; }
        public T[] results { get; set; }

        public override string ToString()
        {
            return $"{count} results";
        }
    }
}
