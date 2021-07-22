using AniAPI.NET.Models;
using AniAPI.NET.Models.Resources;
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
    public class ResourcesTest
    {
        private ITestOutputHelper output;

        public ResourcesTest(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void Get_Genres()
        {
            var result = AniAPI.Instance.GetGenres();

            Assert.NotNull(result);
            Assert.IsType<APIResponse<GenresResource>>(result);

            Assert.True(result.StatusCode == 200);
            Assert.NotNull(result.Data);

            output.WriteLine(JsonConvert.SerializeObject(result));
        }

        [Fact]
        public void Get_Localizations()
        {
            var result = AniAPI.Instance.GetLocalizations();

            Assert.NotNull(result);
            Assert.IsType<APIResponse<LocalizationsResource>>(result);

            Assert.True(result.StatusCode == 200);
            Assert.NotNull(result.Data);

            output.WriteLine(JsonConvert.SerializeObject(result));
        }
    }
}
