using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AniAPI.NET.Models
{
    public class APIResponse<T>
    {
        [JsonProperty("status_code")]
        public int StatusCode { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("data")]
        public T Data { get; set; }

        [JsonProperty("version")]
        public string Version { get; set; }
    }
}
