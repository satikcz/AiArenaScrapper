namespace AIArenaScrapper
{
    public class AIArenaWebClient : IDisposable
    {
        // https://aiarena.net/swagger/

        private HttpClient client = new HttpClient();
        private bool authed = false;

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

        public async Task<T?> GetAsync<T>(string url)
        {
            var str = await GetAsyncString(url) ?? string.Empty;

            if (string.IsNullOrEmpty(str))
                return default;

            return JsonSerializer.Deserialize<T>(str) ?? default;
        }

        public Task<Stream> GetStreamAsync(string url)
        {
            return client.GetStreamAsync(url);
        }
    }
}
