using AniAPI.NET.Enums;
using AniAPI.NET.Filters;
using AniAPI.NET.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace AniAPI.NET.Test
{
    public class EpisodeTest
    {
        private ITestOutputHelper output;

        public EpisodeTest(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public async void Get_Episode()
        {
            long id = 1;

            var result = await AniAPI.Instance.GetEpisode(id);

            Assert.NotNull(result);
            Assert.IsType<APIResponse<Episode>>(result);

            Assert.True(result.StatusCode == 200);
            Assert.NotNull(result.Data);

            Assert.Equal(id, result.Data.Id);

            output.WriteLine(JsonConvert.SerializeObject(result));
        }

        [Fact]
        public async void Get_Episode_List()
        {
            EpisodeFilter filter = new EpisodeFilter()
            {
                AnimeId = 11,
                IsDub = true,
                Locale = "en",
                Sort = new Dictionary<string, SortDirectionEnum>()
                {
                    { "number", SortDirectionEnum.DESCENDING }
                }
            };

            var result = await AniAPI.Instance.GetEpisodeList(filter);

            Assert.NotNull(result);
            Assert.IsType<APIResponse<Pagination<Episode>>>(result);

            Assert.True(result.StatusCode == 200);
            Assert.NotNull(result.Data);

            output.WriteLine(JsonConvert.SerializeObject(result));
        }
    }
}
