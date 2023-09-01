using System.Text.Json.Serialization;

namespace AIArenaScrapper.Payloads
{
    public class Bot
    {
        public int id { get; set; }
        public int user { get; set; }
        public string name { get; set; }
        public DateTime created { get; set; }
        public string bot_zip { get; set; }
        public DateTime bot_zip_updated { get; set; }
        public string bot_zip_md5hash { get; set; }
        public bool bot_zip_publicly_downloadable { get; set; }
        public bool bot_data_enabled { get; set; }
        public string bot_data { get; set; }
        public string bot_data_md5hash { get; set; }
        public bool bot_data_publicly_downloadable { get; set; }
        public BotRace plays_race { get; set; }
        public string type { get; set; }
        public string game_display_id { get; set; }
        public IEnumerable<Trophy> trophies { get; set; }
        
        [JsonIgnore]
        public User? User { get; set; }

        public override string ToString()
        {
            return $"#{id} {name} ({plays_race.label}, {type}), download: {bot_zip_publicly_downloadable} by {User}";
        }
    }
}
