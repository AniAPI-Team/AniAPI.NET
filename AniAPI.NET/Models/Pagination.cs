using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AniAPI.NET.Models
{
    public class Pagination<T> where T : class, new()
    {
        [JsonProperty("current_page")]
        public int CurrentPage { get; set; }

        [JsonProperty("count")]
        public int count { get; set; }

        [JsonProperty("documents")]
        public List<T> Documents { get; set; }

        [JsonProperty("last_page")]
        public int LastPage { get; set; }
    }
}
