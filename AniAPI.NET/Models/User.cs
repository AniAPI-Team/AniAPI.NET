using AniAPI.NET.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AniAPI.NET.Models
{
    public class User
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }

        [JsonProperty("email_verified")]
        public bool? EmailVerified { get; set; }

        [JsonProperty("last_login_date")]
        public DateTime? LastLoginDate { get; set; }

        [JsonProperty("access_token")]
        public string Token { get; set; }

        [JsonProperty("role")]
        public UserRoleEnum Role { get; set; }

        [JsonProperty("gender")]
        public UserGenderEnum Gender { get; set; }

        [JsonProperty("avatar")]
        public string Avatar { get; set; }

        [JsonProperty("localization")]
        public string Localization { get; set; }

        [JsonProperty("anilist_id")]
        public long? AnilistId { get; set; }

        [JsonProperty("anilist_token")]
        public string AnilistToken { get; set; }

        [JsonProperty("has_anilist")]
        public bool? HasAnilist { get; set; } = null;

        [JsonProperty("mal_id")]
        public long? MyAnimeListId { get; set; }

        [JsonProperty("mal_token")]
        public string MyAnimeListToken { get; set; }

        [JsonProperty("has_mal")]
        public bool? HasMyAnimeList { get; set; } = null;

        public override string ToString()
        {
            return Username;
        }
    }
}
