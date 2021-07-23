using AniAPI.NET.Enums;
using AniAPI.NET.Filters;
using AniAPI.NET.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace AniAPI.NET.Test
{
    public class UserStoryTest
    {
        private readonly IConfiguration configuration;
        private ITestOutputHelper output;

        public UserStoryTest(ITestOutputHelper output)
        {
            this.output = output;

            this.configuration = new ConfigurationBuilder().
                SetBasePath(Directory.GetCurrentDirectory()).
                AddJsonFile("appsettings.json").
                Build();
        }

        [Fact]
        public async void Get_UserStory()
        {
            long id = 1;

            AniAPI.Instance.ManualJWT(this.configuration["JWT"]);

            var result = await AniAPI.Instance.GetUserStory(id);

            Assert.NotNull(result);
            Assert.IsType<APIResponse<UserStory>>(result);

            Assert.True(result.StatusCode == 200);
            Assert.NotNull(result.Data);

            Assert.Equal(id, result.Data.Id);

            output.WriteLine(JsonConvert.SerializeObject(result));
        }

        [Fact]
        public async void Get_UserStory_List()
        {
            UserStoryFilter filter = new UserStoryFilter()
            {
                AnimeId = 11,
                UserId = 1,
                Sort = new Dictionary<string, SortDirectionEnum>()
                {
                    { "anime_id", SortDirectionEnum.ASCENDING }
                }
            };

            AniAPI.Instance.ManualJWT(this.configuration["JWT"]);

            var result = await AniAPI.Instance.GetUserStoryList(filter);

            Assert.NotNull(result);
            Assert.IsType<APIResponse<Pagination<UserStory>>>(result);

            Assert.True(result.StatusCode == 200);
            Assert.NotNull(result.Data);

            output.WriteLine(JsonConvert.SerializeObject(result));
        }

        [Fact]
        public async void Create_UserStory()
        {
            UserStory model = new UserStory()
            {
                UserId = 1,
                AnimeId = 11,
                Status = UserStoryStatusEnum.PAUSED,
                CurrentEpisode = 897
            };

            AniAPI.Instance.ManualJWT(this.configuration["JWT"]);

            var result = await AniAPI.Instance.CreateUserStory(model);

            Assert.NotNull(result);
            Assert.IsType<APIResponse<UserStory>>(result);

            Assert.True(result.StatusCode == 200);
            Assert.NotNull(result.Data);

            output.WriteLine(JsonConvert.SerializeObject(result));
        }

        [Fact]
        public async void Update_UserStory()
        {
            UserStory model = new UserStory()
            {
                Id = 60,
                UserId = 1,
                AnimeId = 11,
                Status = UserStoryStatusEnum.PAUSED,
                CurrentEpisode = 897
            };

            AniAPI.Instance.ManualJWT(this.configuration["JWT"]);

            var result = await AniAPI.Instance.UpdateUserStory(model);

            Assert.NotNull(result);
            Assert.IsType<APIResponse<UserStory>>(result);

            Assert.True(result.StatusCode == 200);
            Assert.NotNull(result.Data);

            output.WriteLine(JsonConvert.SerializeObject(result));
        }
    }
}
