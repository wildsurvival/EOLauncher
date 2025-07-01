using Avalonia.Controls.Converters;
using Avalonia.Media;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EOLauncher.Models
{
    public class Server
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("host")]
        public string Host { get; set; }
        [JsonPropertyName("port")]
        public int Port { get; set; } = 8078;
        [JsonPropertyName("players")]
        public int Players { get; set; } = 0;

        public string Status { get; set; } = "online";

        [JsonPropertyName("site")]
        public Uri? WebsiteUrl { get; set; } = null;
        [JsonPropertyName("clientsite")]
        public Uri? ClientUrl { get; set; } = null;

        [JsonPropertyName("version")]
        public string Version { get; set; } = string.Empty;

        private string _zone;

        [JsonPropertyName("zone")]
        public string Zone
        {
            get
            {
                TextInfo ti = new CultureInfo("en-US", false).TextInfo;

                return ti.ToTitleCase(string.IsNullOrEmpty(_zone) ? "public" : _zone);
            }
            set => _zone = value;
        }

        public bool HasWebsite
        {
            get => string.IsNullOrEmpty(WebsiteUrl?.ToString()) == false;
        }

        public bool HasClientDownload
        {
            get => string.IsNullOrEmpty(ClientUrl?.ToString()) == false;
        }
    }
}
