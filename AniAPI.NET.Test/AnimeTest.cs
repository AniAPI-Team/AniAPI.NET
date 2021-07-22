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
    public class AnimeTest
    {
        private ITestOutputHelper output;

        public AnimeTest(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void Get_Anime()
        {
            long id = 1;

            var result = AniAPI.Instance.GetAnime(id);

            Assert.NotNull(result);
            Assert.IsType<APIResponse<Anime>>(result);

            Assert.True(result.StatusCode == 200);
            Assert.NotNull(result.Data);

            Assert.Equal(id, result.Data.Id);

            output.WriteLine(JsonConvert.SerializeObject(result));
        }

        [Fact]
        public void Get_Anime_List()
        {
            AnimeFilter filter = new AnimeFilter()
            {
                Title = "Cowboy Bebop",
                AnilistId = 1,
                MyAnimeListId = 1,
                Formats = new AnimeFormatEnum[2]
                {
                    AnimeFormatEnum.TV,
                    AnimeFormatEnum.TV_SHORT
                },
                Status = AnimeStatusEnum.FINISHED,
                Year = 1998,
                Season = AnimeSeasonEnum.SPRING,
                Genres = new string[3]
                {
                    "Action",
                    "Guns",
                    "Military"
                },
                Sort = new Dictionary<string, SortDirectionEnum>()
                {
                    { "titles.en", SortDirectionEnum.DESCENDING }
                }
            };

            var result = AniAPI.Instance.GetAnimeList(filter);

            Assert.NotNull(result);
            Assert.IsType<APIResponse<Pagination<Anime>>>(result);

            Assert.True(result.StatusCode == 200);
            Assert.NotNull(result.Data);

            output.WriteLine(JsonConvert.SerializeObject(result));
        }
    }
}
