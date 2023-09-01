namespace AIArenaScrapper.Filters
{
    public class NameFilter : SearchFilter
    {
        public NameFilter(string name) 
        {
            Add("name", $"{name}");
        }
    }
}
