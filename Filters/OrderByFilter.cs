namespace AIArenaScrapper.Filters
{
    public class OrderByFilter : SearchFilter
    {
        public OrderByFilter(string orderField) 
        {
            Add("ordering", $"{orderField}");
        }
    }
}
