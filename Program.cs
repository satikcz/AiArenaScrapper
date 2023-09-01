global using System.Net.Http.Json;
global using System.Text.Json;
global using System.Text;
global using System.Net.Http.Headers;
global using AIArenaScrapper;
global using AIArenaScrapper.Payloads;
global using AIArenaScrapper.Filters;

Console.WriteLine("Hello, commander!");

// Read AIArena login token from environment variable to keep it away from this source code and prevent accidental pushing it :) 
// But feel free to just hardcode it here for your usage.
var aiArenaToken = Environment.GetEnvironmentVariable("aiarena_token") ?? string.Empty;

var provider = new ArenaProvider(aiArenaToken);

// Download all downloadable bots into bots folder
/*var downloader = new DownloadableBotDownloader("bots", provider);
await downloader.Run();//*/

// Print detailed bot stats
var detailed = new DetailedBotStats(provider, "DadBot", "Sc2 AI Arena 2023 Season 2");
await detailed.Run();//*/

/*var users = await provider.GetUsersAsync();

var botCount = await provider.GetBotCountAsync();
var bots = await provider.GetBotsAsync();

Console.WriteLine($"Bot count: {botCount}");

foreach (var bot in bots)
    Console.WriteLine(bot);

var competitions = await provider.GetCompetitionsAsync();
var matches = await provider.GetMatchesAsync();
var results = await provider.GetResultsAsync();
var rounds = await provider.GetRoundsAsync();
*/