﻿namespace AIArenaScrapper.Payloads
{
    public class Trophy
    {
        public int id { get; set; }
        public int icon { get; set; }
        public string name { get; set; }
        public int bot { get; set; }
        public string trophy_icon_name { get; set; }
        public string trophy_icon_image { get; set; }

        public override string ToString()
        {
            return $"#{id} {name} {trophy_icon_name} img:{trophy_icon_image}";
        }
    }
}
