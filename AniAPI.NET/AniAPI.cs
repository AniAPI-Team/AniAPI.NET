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

        public APIResponse<Anime> GetAnime(long id)
        {
            return _httpHelper.UnauthorizedRequest<Anime>($"anime/{id}", HttpMethod.Get);
        }

        public APIResponse<Pagination<Anime>> GetAnimeList(AnimeFilter filter)
        {
            return _httpHelper.UnauthorizedRequest<Pagination<Anime>>($"anime/{filter.ToQueryString()}", HttpMethod.Get);
        }

        public APIResponse<Episode> GetEpisode(long id)
        {
            return _httpHelper.UnauthorizedRequest<Episode>($"episode/{id}", HttpMethod.Get);
        }

        public APIResponse<Pagination<Episode>> GetEpisodeList(EpisodeFilter filter)
        {
            return _httpHelper.UnauthorizedRequest<Pagination<Episode>>($"episode/{filter.ToQueryString()}", HttpMethod.Get);
        }

        public APIResponse<Song> GetSong(long id)
        {
            return _httpHelper.UnauthorizedRequest<Song>($"song/{id}", HttpMethod.Get);
        }

        public APIResponse<Pagination<Song>> GetSongList(SongFilter filter)
        {
            return _httpHelper.UnauthorizedRequest<Pagination<Song>>($"song/{filter.ToQueryString()}", HttpMethod.Get);
        }

        public APIResponse<GenresResource> GetGenres(string version = "1.0")
        {
            return _httpHelper.UnauthorizedRequest<GenresResource>($"resources/{version}/0", HttpMethod.Get);
        }

        public APIResponse<LocalizationsResource> GetLocalizations(string version = "1.0")
        {
            return _httpHelper.UnauthorizedRequest<LocalizationsResource>($"resources/{version}/1", HttpMethod.Get);
        }

        public APIResponse<User> GetUser(long id)
        {
            return _httpHelper.UnauthorizedRequest<User>($"user/{id}", HttpMethod.Get);
        }

        public APIResponse<Pagination<User>> GetUserList(UserFilter filter)
        {
            return _httpHelper.UnauthorizedRequest<Pagination<User>>($"user/{filter.ToQueryString()}", HttpMethod.Get);
        }

        public APIResponse<User> UpdateUser(User model)
        {
            return _httpHelper.AuthorizedRequest<User>("user", HttpMethod.Post, JsonConvert.SerializeObject(model));
        }

        public APIResponse<string> DeleteUser(long id)
        {
            return _httpHelper.AuthorizedRequest<string>($"user/{id}", HttpMethod.Delete);
        }

        public APIResponse<UserStory> GetUserStory(long id)
        {
            return _httpHelper.AuthorizedRequest<UserStory>($"user_story/{id}", HttpMethod.Get);
        }

        public APIResponse<Pagination<UserStory>> GetUserStoryList(UserStoryFilter filter)
        {
            return _httpHelper.AuthorizedRequest<Pagination<UserStory>>($"user_story/{filter.ToQueryString()}", HttpMethod.Get);
        }

        public APIResponse<UserStory> CreateUserStory(UserStory model)
        {
            return _httpHelper.AuthorizedRequest<UserStory>("user_story", HttpMethod.Put, JsonConvert.SerializeObject(model));
        }

        public APIResponse<UserStory> UpdateUserStory(UserStory model)
        {
            return _httpHelper.AuthorizedRequest<UserStory>("user_story", HttpMethod.Post, JsonConvert.SerializeObject(model));
        }

        public APIResponse<string> DeleteUserStory(long id)
        {
            return _httpHelper.AuthorizedRequest<string>($"user_story/{id}", HttpMethod.Delete);
        }

        #endregion
    }
}
