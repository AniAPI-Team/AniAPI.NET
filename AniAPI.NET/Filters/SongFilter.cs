using AniAPI.NET.Enums;
using AniAPI.NET.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AniAPI.NET.Filters
{
    public class SongFilter : IFilter
    {
        public long? AnimeId { get; set; }
        public string Title { get; set; }
        public string Artist { get; set; }
        public int? Year { get; set; }
        public AnimeSeasonEnum? Season { get; set; }
        public SongTypeEnum? Type { get; set; }

        protected override void FillQueryParameters()
        {
            if (AnimeId != null)
            {
                Parameters.Add("anime_id", AnimeId.ToString());
            }

            if (!string.IsNullOrEmpty(Title))
            {
                Parameters.Add("title", Title);
            }

            if (!string.IsNullOrEmpty(Artist))
            {
                Parameters.Add("artist", Artist);
            }

            if (Year != null)
            {
                Parameters.Add("year", Year.ToString());
            }

            if (Season != null)
            {
                Parameters.Add("season", ((int)Season).ToString());
            }

            if (Type != null)
            {
                Parameters.Add("type", ((int)Type).ToString());
            }
        }
    }
}
