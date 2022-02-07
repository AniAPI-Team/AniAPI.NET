using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AniAPI.NET.Models
{
    public class Episode
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("anime_id")]
        public long AnimeId { get; set; }

        [JsonProperty("number")]
        public int Number { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("video")]
        public string Video { get; set; }

        [JsonProperty("quality")]
        public string Quality { get; set; }

        [JsonProperty("format")]
        public string Format { get; set; }

        [JsonProperty("is_dub")]
        public bool IsDub { get; set; }

        [JsonProperty("locale")]
        public string Locale { get; set; }

        public override string ToString()
        {
            return string.IsNullOrEmpty(Title) ? $"Episode {Number}" : Title;
        }
    }
}
