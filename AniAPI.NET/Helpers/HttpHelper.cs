using AniAPI.NET.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AniAPI.NET.Helpers
{
    public class HttpHelper
    {
        private string _protocol;
        private string _hostName;
        private string _version;

        private string _jwt;

        private HttpClient _httpClient;

        public HttpHelper()
        {
            _protocol = "https";
            _hostName = "api.aniapi.com";
            _version = "v1";

            _httpClient = new HttpClient();
        }

        private string endpoint => $"{_protocol}://{_hostName}/{_version}";

        private APIResponse<T> executeRequest<T>(string path, HttpMethod method, string body = null, bool auth = false)
        {
            try
            {
                string uri = $"{endpoint}/{path}";

                HttpRequestMessage request = new HttpRequestMessage(method, uri);

                if (auth)
                {
                    request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _jwt);
                }

                if (!string.IsNullOrEmpty(body))
                {
                    request.Content = new StringContent(body, Encoding.UTF8, "application/json");
                }

                HttpResponseMessage response = _httpClient.SendAsync(request).Result;

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception(response.StatusCode.ToString());
                }

                string responseContent = response.Content.ReadAsStringAsync().Result;

                APIResponse<T> result = JsonConvert.DeserializeObject<APIResponse<T>>(responseContent);

                if(result.StatusCode != 200)
                {
                    throw new Exception($"{result.StatusCode}: {result.Message}");
                }

                return result;
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        public void UseHTTP()
        {
            _protocol = "http";
        }

        public void UseHTTPS()
        {
            _protocol = "https";
        }

        public void SetJWT(string jwt)
        {
            _jwt = jwt;
        }

        public APIResponse<T> UnauthorizedRequest<T>(string path, HttpMethod method)
        {
            return executeRequest<T>(path, method);
        }

        public APIResponse<T> AuthorizedRequest<T>(string path, HttpMethod method, string body = null)
        {
            if (string.IsNullOrEmpty(_jwt))
            {
                throw new ArgumentException("You need to login to perform this request", "JWT");
            }

            return executeRequest<T>(path, method, body, true);
        }
    }
}
