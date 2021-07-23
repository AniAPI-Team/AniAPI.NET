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
    public class UserTest
    {
        private readonly IConfiguration configuration;
        private ITestOutputHelper output;

        public UserTest(ITestOutputHelper output)
        {
            this.output = output;

            this.configuration = new ConfigurationBuilder().
                SetBasePath(Directory.GetCurrentDirectory()).
                AddJsonFile("appsettings.json").
                Build();
        }

        [Fact]
        public async void Get_User()
        {
            long id = 1;

            var result = await AniAPI.Instance.GetUser(id);

            Assert.NotNull(result);
            Assert.IsType<APIResponse<User>>(result);

            Assert.True(result.StatusCode == 200);
            Assert.NotNull(result.Data);

            Assert.Equal(id, result.Data.Id);

            output.WriteLine(JsonConvert.SerializeObject(result));
        }

        [Fact]
        public async void Get_User_List()
        {
            UserFilter filter = new UserFilter()
            {
                Username = "Daz",
                Sort = new Dictionary<string, SortDirectionEnum>()
                {
                    { "username", SortDirectionEnum.ASCENDING }
                }
            };

            var result = await AniAPI.Instance.GetUserList(filter);

            Assert.NotNull(result);
            Assert.IsType<APIResponse<Pagination<User>>>(result);

            Assert.True(result.StatusCode == 200);
            Assert.NotNull(result.Data);

            output.WriteLine(JsonConvert.SerializeObject(result));
        }

        [Fact]
        public async void Update_User()
        {
            User model = new User()
            {
                Id = 1,
                Gender = UserGenderEnum.MALE,
                Localization = "it"
            };

            AniAPI.Instance.ManualJWT(this.configuration["JWT"]);

            var result = await AniAPI.Instance.UpdateUser(model);

            Assert.NotNull(result);
            Assert.IsType<APIResponse<User>>(result);

            Assert.True(result.StatusCode == 200);
            Assert.NotNull(result.Data);

            output.WriteLine(JsonConvert.SerializeObject(result));
        }
    }
}
