using AniAPI.NET.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AniAPI.NET.Models
{
    public class Anime
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("anilist_id")]
        public long AnilistId { get; set; }

        [JsonProperty("mal_id")]
        public long MyAnimeListId { get; set; }

        [JsonProperty("format")]
        public AnimeFormatEnum Format { get; set; }

        [JsonProperty("status")]
        public AnimeStatusEnum Status { get; set; }

        [JsonProperty("titles")]
        public Dictionary<string, string> Titles { get; set; }

        [JsonProperty("descriptions")]
        public Dictionary<string, string> Descriptions { get; set; }

        [JsonProperty("start_date")]
        public DateTime? StartDate { get; set; }

        [JsonProperty("end_date")]
        public DateTime? EndDate { get; set; }

        [JsonProperty("season_period")]
        public AnimeSeasonEnum SeasonPeriod { get; set; }

        [JsonProperty("season_year")]
        public int? SeasonYear { get; set; }

        [JsonProperty("episodes_count")]
        public int EpisodesCount { get; set; }

        [JsonProperty("episode_duration")]
        public int? EpisodeDuration { get; set; }

        [JsonProperty("trailer_url")]
        public string TrailerUrl { get; set; }

        [JsonProperty("cover_image")]
        public string CoverImage { get; set; }

        [JsonProperty("cover_color")]
        public string CoverColor { get; set; }

        [JsonProperty("banner_image")]
        public string BannerImage { get; set; }

        [JsonProperty("genres")]
        public List<string> Genres { get; set; }

        [JsonProperty("sequel")]
        public long? Sequel { get; set; }

        [JsonProperty("prequel")]
        public long? Prequel { get; set; }

        [JsonProperty("score")]
        public int Score { get; set; }

        public override string ToString()
        {
            return Titles["en"];
        }
    }
}
