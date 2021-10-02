using AniAPI.NET.Enums;
using AniAPI.NET.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AniAPI.NET.Filters
{
    public class UserStoryFilter : IFilter
    {
        public long? AnimeId { get; set; }
        public long? UserId { get; set; }
        public UserStoryStatusEnum? Status { get; set; }
        public bool? Synced { get; set; }

        protected override void FillQueryParameters()
        {
            if(AnimeId != null)
            {
                Parameters.Add("anime_id", AnimeId.ToString());
            }

            if (UserId != null)
            {
                Parameters.Add("user_id", UserId.ToString());
            }

            if (Status != null)
            {
                Parameters.Add("status", ((int)Status).ToString());
            }

            if (Synced != null)
            {
                Parameters.Add("synced", Synced.Value ? "true" : "false");
            }
        }
    }
}
