namespace AIArenaScrapper.Filters
{
    /// <summary>
    /// Search filter abstracts the url search queries.
    /// You can add key/value pairs into the filter.
    /// Some filters are predefined.
    /// You can also merge multiple filters with the + operator.
    /// </summary>
    public class SearchFilter : Dictionary<string, string>
    {
        public static SearchFilter operator+(SearchFilter left, SearchFilter right)
        {
            var merged = new SearchFilter();

            foreach (var entry in left) 
            {
                merged[entry.Key] = entry.Value;
            }

            foreach (var entry in right)
            {
                merged[entry.Key] = entry.Value;
            }

            return merged;
        }
    }
}
