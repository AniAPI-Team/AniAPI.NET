using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AniAPI.NET.Models.Resources
{
    public class GenresResource
    {
        [JsonProperty("genres")]
        public List<string> Genres { get; set; }
    }
}
