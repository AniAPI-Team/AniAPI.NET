using AniAPI.NET.Filters;
using AniAPI.NET.Models;
using AniAPI.NET.Models.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AniAPI.NET.Interfaces
{
    public interface IAniAPI
    {
        #region OAuth

        public Task Login();
        public Task<APIResponse<User>> GetMe();

        #endregion

        #region Anime

        public Task<APIResponse<Anime>> GetAnime(long id);
        public Task<APIResponse<Pagination<Anime>>> GetAnimeList(AnimeFilter filter);

        #endregion

        #region Episode

        public Task<APIResponse<Episode>> GetEpisode(long id);
        public Task<APIResponse<Pagination<Episode>>> GetEpisodeList(EpisodeFilter filter);

        #endregion

        #region Song

        public Task<APIResponse<Song>> GetSong(long id);
        public Task<APIResponse<Pagination<Song>>> GetSongList(SongFilter filter);

        #endregion

        #region Resource

        public Task<APIResponse<GenresResource>> GetGenres(string version = "1.0");
        public Task<APIResponse<LocalizationsResource>> GetLocalizations(string version = "1.0");

        #endregion

        #region User

        public Task<APIResponse<User>> GetUser(long id);
        public Task<APIResponse<Pagination<User>>> GetUserList(UserFilter filter);
        public Task<APIResponse<User>> UpdateUser(User model);
        public Task<APIResponse<string>> DeleteUser(long id);

        #endregion

        #region UserStory

        public Task<APIResponse<UserStory>> GetUserStory(long id);
        public Task<APIResponse<Pagination<UserStory>>> GetUserStoryList(UserStoryFilter filter);
        public Task<APIResponse<UserStory>> CreateUserStory(UserStory model);
        public Task<APIResponse<UserStory>> UpdateUserStory(UserStory model);
        public Task<APIResponse<string>> DeleteUserStory(long id);

        #endregion
    }
}
