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
    public class SongTest
    {
        private ITestOutputHelper output;

        public SongTest(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public async void Get_Song()
        {
            long id = 1;

            var result = await AniAPI.Instance.GetSong(id);

            Assert.NotNull(result);
            Assert.IsType<APIResponse<Song>>(result);

            Assert.True(result.StatusCode == 200);
            Assert.NotNull(result.Data);

            Assert.Equal(id, result.Data.Id);

            output.WriteLine(JsonConvert.SerializeObject(result));
        }

        [Fact]
        public async void Get_Song_List()
        {
            SongFilter filter = new SongFilter()
            {
                AnimeId = 10,
                Type = SongTypeEnum.OPENING,
                Sort = new Dictionary<string, SortDirectionEnum>()
                {
                    { "year", SortDirectionEnum.DESCENDING },
                    { "season", SortDirectionEnum.ASCENDING }
                }
            };

            var result = await AniAPI.Instance.GetSongList(filter);

            Assert.NotNull(result);
            Assert.IsType<APIResponse<Pagination<Song>>>(result);

            Assert.True(result.StatusCode == 200);
            Assert.NotNull(result.Data);

            output.WriteLine(JsonConvert.SerializeObject(result));
        }
    }
}
