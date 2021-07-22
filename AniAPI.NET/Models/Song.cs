using AniAPI.NET.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AniAPI.NET.Models
{
    public class Song
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("anime_id")]
        public long AnimeId { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("artist")]
        public string Artist { get; set; }

        [JsonProperty("album")]
        public string Album { get; set; }

        [JsonProperty("year")]
        public int Year { get; set; }

        [JsonProperty("season")]
        public AnimeSeasonEnum Season { get; set; }

        [JsonProperty("duration")]
        public int Duration { get; set; }

        [JsonProperty("preview_url")]
        public string PreviewUrl { get; set; }

        [JsonProperty("open_spotify_url")]
        public string OpenSpotifyUrl { get; set; }

        [JsonProperty("local_spotify_url")]
        public string LocalSpotifyUrl { get; set; }

        [JsonProperty("type")]
        public SongTypeEnum SongType { get; set; }

        public override string ToString()
        {
            return $"{Title} - {Artist}";
        }
    }
}
