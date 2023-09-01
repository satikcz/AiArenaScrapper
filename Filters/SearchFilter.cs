using System.Numerics;

namespace AIArenaScrapper.Filters
{
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
