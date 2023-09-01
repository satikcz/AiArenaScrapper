namespace AIArenaScrapper.Filters
{
    public class PubliclyDownloadableFilter : SearchFilter
    {
        public PubliclyDownloadableFilter() 
        {
            Add("bot_zip_publicly_downloadable", $"{true}");
        }
    }
}
