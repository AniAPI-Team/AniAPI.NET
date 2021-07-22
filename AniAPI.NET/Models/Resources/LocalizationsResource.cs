using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AniAPI.NET.Models.Resources
{
    public class LocalizationsResource
    {
        [JsonProperty("localizations")]
        public List<LocalizationResource> Localizations { get; set; }

        public class LocalizationResource
        {
            [JsonProperty("i18n")]
            public string i18n { get; set; }

            [JsonProperty("label")]
            public string Label { get; set; }

            public override string ToString()
            {
                return Label;
            }
        }
    }
}
