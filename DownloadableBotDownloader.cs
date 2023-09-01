namespace AIArenaScrapper
{
    public class DownloadableBotDownloader
    {
        private string _directory;
        private ArenaProvider _arenaProvider;

        public DownloadableBotDownloader(string directory, ArenaProvider arenaProvider)
        {
            _directory = directory;
            _arenaProvider = arenaProvider;
        }

        public async Task Run()
        {
            const int pageSize = 100;
            var count = await _arenaProvider.GetBotCountAsync(new PubliclyDownloadableFilter());

            int offset = 0;
            List<Bot> downloadableBots = new List<Bot>();
            while (offset < count)
            {
                var bots = await _arenaProvider.GetBotsAsync(new PubliclyDownloadableFilter() + new PagedFilter(offset, pageSize));
                downloadableBots.AddRange(bots);
                offset += pageSize;
            }

            if (!Directory.Exists(_directory))
                Directory.CreateDirectory(_directory);

            int downloadCount = 0;
            foreach (var bot in downloadableBots)
            {
                if (await DownloadFileAsync(bot.bot_zip, $"{_directory}/{bot.id}_{bot.name}___{bot.bot_zip_updated.ToString("yyyy:MM:dd:HH:mm:ss").Replace(':', '_')}.zip"))
                    downloadCount++;
            }

            Console.WriteLine($"Downloaded {downloadCount} new bots.");
        }

        private async Task<bool> DownloadFileAsync(string url, string fileName)
        {
            try
            {
                if (File.Exists(fileName))
                {
                    Console.WriteLine($"{fileName} skipped, already downloaded.");
                    return false;
                }

                using var s = await _arenaProvider.WebClient.GetStreamAsync(url);
                using var fs = new FileStream(fileName, FileMode.OpenOrCreate);
                await s.CopyToAsync(fs);
                Console.WriteLine($"{fileName} downloaded successfully.");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{fileName} failed to download");
            }
            return false;
        }
    }
}
