namespace AIArenaScrapper.Filters
{
    public class PagedFilter : SearchFilter
    {
        public PagedFilter(int offset, int limit) 
        {
            Add("offset", $"{offset}");
            Add("limit", $"{limit}");
        }
    }
}
