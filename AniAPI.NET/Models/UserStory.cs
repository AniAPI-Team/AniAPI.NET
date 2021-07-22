using AniAPI.NET.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AniAPI.NET.Models
{
    public class UserStory
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("user_id")]
        public long UserId { get; set; }

        [JsonProperty("anime_id")]
        public long AnimeId { get; set; }

        [JsonProperty("status")]
        public UserStoryStatusEnum Status { get; set; }

        [JsonProperty("current_episode")]
        public int CurrentEpisode { get; set; }

        [JsonProperty("current_episode_ticks")]
        public long? CurrentEpisodeTicks { get; set; }
    }
}
