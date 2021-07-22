using AniAPI.NET.Enums;
using AniAPI.NET.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace AniAPI.NET.Filters
{
    public class AnimeFilter : IFilter
    {
        public string Title { get; set; }
        public long? AnilistId { get; set; }
        public long? MyAnimeListId { get; set; }
        public AnimeFormatEnum[] Formats { get; set; }
        public AnimeStatusEnum? Status { get; set; }
        public int? Year { get; set; }
        public AnimeSeasonEnum? Season { get; set; }
        public string[] Genres { get; set; }

        protected override void FillQueryParameters()
        {
            if (!string.IsNullOrEmpty(Title))
            {
                Parameters.Add("title", HttpUtility.UrlEncode(Title));
            }

            if (AnilistId != null)
            {
                Parameters.Add("anilist_id", AnilistId.ToString());
            }

            if (MyAnimeListId != null)
            {
                Parameters.Add("mal_id", MyAnimeListId.ToString());
            }

            if (Formats != null && Formats.Length > 0)
            {
                Parameters.Add("formats", String.Join(',', Formats.Select(x => ((int)x).ToString())));
            }

            if (Status != null)
            {
                Parameters.Add("status", ((int)Status).ToString());
            }

            if (Year != null)
            {
                Parameters.Add("year", Year.ToString());
            }

            if (Season != null)
            {
                Parameters.Add("season", ((int)Season).ToString());
            }

            if (Genres != null && Genres.Length > 0)
            {
                Parameters.Add("genres", String.Join(',', Genres.Select(x => HttpUtility.UrlEncode(x))));
            }
        }
    }
}
