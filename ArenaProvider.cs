using AIArenaScrapper.Filters;
using System.Net;
using System.Web;

namespace AIArenaScrapper
{
    public class ArenaProvider : IDisposable
    {
        private readonly AIArenaWebClient webClient;
        public AIArenaWebClient WebClient => webClient;

        public ArenaProvider(string token)
        {
            webClient = new AIArenaWebClient(token);
        }

        public void Dispose()
        {
            webClient.Dispose();
        }

        private string AssembleUrl(string url, SearchFilter? filter)
        {
            if (filter is null)
                return url;
            else
                return $"{url}?{string.Join('&', filter.Select(x=>$"{x.Key}={HttpUtility.HtmlEncode(x.Value)}"))}";
        }

        private async Task<int> GetCount<T>(string url, SearchFilter? filter = null)
        {
            return (await webClient.GetAsync<SearchResults<T>>(AssembleUrl(url, filter)))?.count ?? 0;
        }

        private async Task<IEnumerable<T>> GetList<T>(string url, SearchFilter? filter = null)
        {
            var results = await webClient.GetAsync<SearchResults<T>>(AssembleUrl(url, filter));
            return results?.results ?? Enumerable.Empty<T>();
        }

        private async Task<T?> Get<T>(string url)
        {
            return await webClient.GetAsync<T>(url);
        }

        public async Task<int> GetBotCountAsync(SearchFilter? filter = null)
        {
            return await GetCount<Bot>("bots/", filter);
        }

        public async Task<IEnumerable<Bot>> GetBotsAsync(SearchFilter? filter = null, bool obtainUser = true)
        {
            var bots = await GetList<Bot>("bots/", filter);

            foreach (var bot in bots) 
            {
                bot.User = await Get<User>($"users/{bot.user}/");
            }

            return bots;
        }

        public async Task<Bot?> GetBotAsync(int id, bool obtainUser = true)
        {
            var bot = await Get<Bot>($"bots/{id}/");

            if (bot is not null && obtainUser)
                bot.User = await GetUserAsync(bot.user);

            return bot;
        }

        public async Task<int> GetUserCountAsync(SearchFilter? filter = null)
        {
            return await GetCount<User>("users/", filter);
        }

        public async Task<IEnumerable<User>> GetUsersAsync(SearchFilter? filter = null)
        {
            return await GetList<User>("users/", filter);
        }

        public async Task<User?> GetUserAsync(int id)
        {
            return await Get<User>($"users/{id}/");
        }

        public async Task<int> GetCompetitionCountAsync(SearchFilter? filter = null)
        {
            return await GetCount<Competition>("competitions/", filter);
        }

        public async Task<IEnumerable<Competition>> GetCompetitionsAsync(SearchFilter? filter = null)
        {
            return await GetList<Competition>("competitions/", filter);
        }

        public async Task<Competition?> GetCompetitionAsync(int id)
        {
            return await Get<Competition>($"competitions/{id}/");
        }

        public async Task<int> GetMatchCountAsync(SearchFilter? filter = null)
        {
            return await GetCount<Match>("matches/", filter);
        }

        public async Task<IEnumerable<Match>> GetMatchesAsync(SearchFilter? filter = null)
        {
            return await GetList<Match>("matches/", filter);
        }

        public async Task<Match?> GetMatchAsync(int id)
        {
            return await Get<Match>($"matches/{id}/");
        }

        public async Task<int> GetMatchParticipationCountAsync(SearchFilter? filter = null)
        {
            return await GetCount<MatchParticipation>("match-participations/", filter);
        }

        public async Task<IEnumerable<MatchParticipation>> GetMatchParticipationsAsync(SearchFilter? filter = null)
        {
            return await GetList<MatchParticipation>("match-participations/", filter);
        }

        public async Task<MatchParticipation?> GetMatchParticipationAsync(int id)
        {
            return await Get<MatchParticipation>($"match-participations/{id}/");
        }

        public async Task<int> GetResultCountAsync(SearchFilter? filter = null)
        {
            return await GetCount<MatchResult>("results/", filter);
        }

        public async Task<IEnumerable<MatchResult>> GetResultsAsync(SearchFilter? filter = null)
        {
            return await GetList<MatchResult>("results/", filter);
        }

        public async Task<MatchResult?> GetResultAsync(int id)
        {
            return await Get<MatchResult>($"results/{id}/");
        }

        public async Task<int> GetRoundCountAsync(SearchFilter? filter = null)
        {
            return await GetCount<Round>("rounds/", filter);
        }

        public async Task<IEnumerable<Round>> GetRoundsAsync(SearchFilter? filter = null)
        {
            return await GetList<Round>("rounds/", filter);
        }

        public async Task<Round?> GetRoundAsync(int id)
        {
            return await Get<Round>($"rounds/{id}/");
        }

        public async Task<int> GetMapCountAsync(SearchFilter? filter = null)
        {
            return await GetCount<Map>("maps/", filter);
        }

        public async Task<IEnumerable<Map>> GetMapsAsync(SearchFilter? filter = null)
        {
            return await GetList<Map>("maps/", filter);
        }

        public async Task<Map?> GetMapAsync(int id)
        {
            return await Get<Map>($"maps/{id}/");
        }
    }
}
