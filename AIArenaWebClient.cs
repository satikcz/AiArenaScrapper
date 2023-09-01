namespace AIArenaScrapper
{
    /// <summary>
    /// Web client that makes easier communication with the AIArena REST API
    /// </summary>
    public class AIArenaWebClient : IDisposable
    {
        // https://aiarena.net/swagger/

        private HttpClient client = new HttpClient();

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="apiToken">AIArena token, can be found in https://aiarena.net/profile/token/?</param>
        /// <param name="url">AIArena url</param>
        public AIArenaWebClient(string apiToken, string url = "https://aiarena.net/api/")
        {
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Token", apiToken);
        }

        public void Dispose()
        {
            client.Dispose();
        }

        /// <summary>
        /// Gets string result from given URL via GET
        /// </summary>
        public async Task<string?> GetAsyncString(string url)
        {
            try
            {
                var response = await client.GetAsync(url);
                response?.EnsureSuccessStatusCode();

                if (response is not null)
                {
                    var ret = await response.Content.ReadAsStringAsync() ?? string.Empty;
                    return ret;
                }
            }
            catch { }

            return string.Empty;
        }

        /// <summary>
        /// Gets object parsed from JSON string obtained from given URL via GET
        /// </summary>
        /// <typeparam name="T">Type of object to parse the JSON into</typeparam>
        public async Task<T?> GetAsync<T>(string url)
        {
            var str = await GetAsyncString(url) ?? string.Empty;

            if (string.IsNullOrEmpty(str))
                return default;

            return JsonSerializer.Deserialize<T>(str) ?? default;
        }

        /// <summary>
        /// Gets stream obtained from URL via GET
        /// </summary>
        public Task<Stream> GetStreamAsync(string url)
        {
            return client.GetStreamAsync(url);
        }
    }
}
