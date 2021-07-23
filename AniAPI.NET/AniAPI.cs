using AniAPI.NET.Filters;
using AniAPI.NET.Helpers;
using AniAPI.NET.Interfaces;
using AniAPI.NET.Models;
using AniAPI.NET.Models.Resources;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AniAPI.NET
{
    public class AniAPI : IAniAPI
    {
        private HttpHelper _httpHelper;
        private OAuthHelper _oAuthHelper;

        #region Singleton

        private static AniAPI _instance;

        public static AniAPI Instance
        {
            get
            {
                if(_instance == null)
                {
                    _instance = new AniAPI();
                }

                return _instance;
            }
        }

        private AniAPI()
        {
            _httpHelper = new HttpHelper();
        }

        #endregion

        public void UseHTTP()
        {
            _httpHelper.UseHTTP();
        }

        public void UseHTTPS()
        {
            _httpHelper.UseHTTPS();
        }

        public void UseImplicitGrant(string clientId, string redirectUri)
        {
            _oAuthHelper = new OAuthHelper(clientId, redirectUri);
        }

        public void UseAuthorizationCode(string clientId, string clientSecret, string redirectUri)
        {
            _oAuthHelper = new OAuthHelper(clientId, clientSecret, redirectUri);
        }

        public async Task Login()
        {
            if(_oAuthHelper == null)
            {
                throw new Exception("OAuth not setted up!");
            }

            string token = await _oAuthHelper.Login();

            if (string.IsNullOrEmpty(token))
            {
                throw new Exception("Token not valid!");
            }

            _httpHelper.SetJWT(token);
        }

        public void ManualJWT(string jwt)
        {
            _httpHelper.SetJWT(jwt);
        }

        #region Implementation

        public async Task<APIResponse<Anime>> GetAnime(long id)
        {
            return await _httpHelper.UnauthorizedRequest<Anime>($"anime/{id}", HttpMethod.Get);
        }

        public async Task<APIResponse<Pagination<Anime>>> GetAnimeList(AnimeFilter filter)
        {
            return await _httpHelper.UnauthorizedRequest<Pagination<Anime>>($"anime/{filter.ToQueryString()}", HttpMethod.Get);
        }

        public async Task<APIResponse<Episode>> GetEpisode(long id)
        {
            return await _httpHelper.UnauthorizedRequest<Episode>($"episode/{id}", HttpMethod.Get);
        }

        public async Task<APIResponse<Pagination<Episode>>> GetEpisodeList(EpisodeFilter filter)
        {
            return await _httpHelper.UnauthorizedRequest<Pagination<Episode>>($"episode/{filter.ToQueryString()}", HttpMethod.Get);
        }

        public async Task<APIResponse<Song>> GetSong(long id)
        {
            return await _httpHelper.UnauthorizedRequest<Song>($"song/{id}", HttpMethod.Get);
        }

        public async Task<APIResponse<Pagination<Song>>> GetSongList(SongFilter filter)
        {
            return await _httpHelper.UnauthorizedRequest<Pagination<Song>>($"song/{filter.ToQueryString()}", HttpMethod.Get);
        }

        public async Task<APIResponse<GenresResource>> GetGenres(string version = "1.0")
        {
            return await _httpHelper.UnauthorizedRequest<GenresResource>($"resources/{version}/0", HttpMethod.Get);
        }

        public async Task<APIResponse<LocalizationsResource>> GetLocalizations(string version = "1.0")
        {
            return await _httpHelper.UnauthorizedRequest<LocalizationsResource>($"resources/{version}/1", HttpMethod.Get);
        }

        public async Task<APIResponse<User>> GetUser(long id)
        {
            return await _httpHelper.UnauthorizedRequest<User>($"user/{id}", HttpMethod.Get);
        }

        public async Task<APIResponse<Pagination<User>>> GetUserList(UserFilter filter)
        {
            return await _httpHelper.UnauthorizedRequest<Pagination<User>>($"user/{filter.ToQueryString()}", HttpMethod.Get);
        }

        public async Task<APIResponse<User>> UpdateUser(User model)
        {
            return await _httpHelper.AuthorizedRequest<User>("user", HttpMethod.Post, JsonConvert.SerializeObject(model));
        }

        public async Task<APIResponse<string>> DeleteUser(long id)
        {
            return await _httpHelper.AuthorizedRequest<string>($"user/{id}", HttpMethod.Delete);
        }

        public async Task<APIResponse<UserStory>> GetUserStory(long id)
        {
            return await _httpHelper.AuthorizedRequest<UserStory>($"user_story/{id}", HttpMethod.Get);
        }

        public async Task<APIResponse<Pagination<UserStory>>> GetUserStoryList(UserStoryFilter filter)
        {
            return await _httpHelper.AuthorizedRequest<Pagination<UserStory>>($"user_story/{filter.ToQueryString()}", HttpMethod.Get);
        }

        public async Task<APIResponse<UserStory>> CreateUserStory(UserStory model)
        {
            return await _httpHelper.AuthorizedRequest<UserStory>("user_story", HttpMethod.Put, JsonConvert.SerializeObject(model));
        }

        public async Task<APIResponse<UserStory>> UpdateUserStory(UserStory model)
        {
            return await _httpHelper.AuthorizedRequest<UserStory>("user_story", HttpMethod.Post, JsonConvert.SerializeObject(model));
        }

        public async Task<APIResponse<string>> DeleteUserStory(long id)
        {
            return await _httpHelper.AuthorizedRequest<string>($"user_story/{id}", HttpMethod.Delete);
        }

        #endregion
    }
}
