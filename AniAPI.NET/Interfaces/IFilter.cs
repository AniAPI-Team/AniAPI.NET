using AniAPI.NET.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AniAPI.NET.Interfaces
{
    public abstract class IFilter
    {
        protected Dictionary<string, string> Parameters = new Dictionary<string, string>();

        public long[] Ids { get; set; }
        public Dictionary<string, SortDirectionEnum> Sort { get; set; }

        protected abstract void FillQueryParameters();

        public string ToQueryString()
        {
            string queryString = string.Empty;

            if (Ids != null && Ids.Length > 0)
            {
                Parameters.Add("ids", String.Join(',', Ids.Select(x => x.ToString())));
            }

            FillQueryParameters();

            if(Sort != null && Sort.Keys.Count > 0)
            {
                Parameters.Add("sort_fields", String.Join(',', Sort.Keys.Select(x => x)));
                Parameters.Add("sort_directions", String.Join(',', Sort.Values.Select(x => ((int)x).ToString())));
            }

            if(Parameters.Count > 0)
            {
                queryString = "?" + String.Join('&', Parameters.Select(x => $"{x.Key}={x.Value}"));
            }

            return queryString;
        }
    }
}
