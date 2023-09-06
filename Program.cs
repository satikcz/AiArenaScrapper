global using System.Net.Http.Json;
global using System.Text.Json;
global using System.Text;
global using System.Net.Http.Headers;
global using AIArenaScrapper;
global using AIArenaScrapper.Payloads;
global using AIArenaScrapper.Filters;
global using System.Net;
global using System.Web;

Console.WriteLine("Hello, commander!");

// Read AIArena login token from environment variable to keep it away from this source code and prevent accidental pushing it :) 
// But feel free to just hardcode it here for your usage.
// NOTE: if you update the env variable and you are using Visual Studio, you need to restart Visual Studio for it to register the variable was changed.
// The token can be found in https://aiarena.net/profile/token/?
var aiArenaToken = Environment.GetEnvironmentVariable("aiarena_token") ?? string.Empty;

// Create provider that is used to obtain data from then AIArena
var provider = new ArenaProvider(aiArenaToken);

// Download all downloadable bots into bots folder
var downloader = new DownloadableBotDownloader("bots", provider);
await downloader.Run();

// Print detailed bot stats
var detailed = new DetailedBotStats(provider, "DadBot", "Sc2 AI Arena 2023 Season 2");
await detailed.Run();

/*
// You can request various data from the AIArena in following manner:
var users = await provider.GetUsersAsync();
var botCount = await provider.GetBotCountAsync();
var bots = await provider.GetBotsAsync();
var competitions = await provider.GetCompetitionsAsync();
var matches = await provider.GetMatchesAsync();
var results = await provider.GetResultsAsync();
var rounds = await provider.GetRoundsAsync();
*/