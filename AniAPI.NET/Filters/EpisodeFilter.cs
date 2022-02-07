using AniAPI.NET.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AniAPI.NET.Filters
{
    public class EpisodeFilter : IFilter
    {
        public long? AnimeId { get; set; }
        public int? Number { get; set; }
        public bool? IsDub { get; set; }
        public string Locale { get; set; }

        protected override void FillQueryParameters()
        {
            if(AnimeId != null)
            {
                Parameters.Add("anime_id", AnimeId.ToString());
            }

            if (Number != null)
            {
                Parameters.Add("number", Number.ToString());
            }

            if (IsDub != null)
            {
                Parameters.Add("is_dub", IsDub.Value ? "true" : "false");
            }

            if (!string.IsNullOrEmpty(Locale))
            {
                Parameters.Add("locale", Locale);
            }
        }
    }
}
