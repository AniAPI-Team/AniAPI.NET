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



        #endregion

        #region Anime

        public APIResponse<Anime> GetAnime(long id);
        public APIResponse<Pagination<Anime>> GetAnimeList(AnimeFilter filter);

        #endregion

        #region Episode

        public APIResponse<Episode> GetEpisode(long id);
        public APIResponse<Pagination<Episode>> GetEpisodeList(EpisodeFilter filter);

        #endregion

        #region Song

        public APIResponse<Song> GetSong(long id);
        public APIResponse<Pagination<Song>> GetSongList(SongFilter filter);

        #endregion

        #region Resource

        public APIResponse<GenresResource> GetGenres(string version = "1.0");
        public APIResponse<LocalizationsResource> GetLocalizations(string version = "1.0");

        #endregion

        #region User

        public APIResponse<User> GetUser(long id);
        public APIResponse<Pagination<User>> GetUserList(UserFilter filter);
        public APIResponse<User> UpdateUser(User model);
        public APIResponse<string> DeleteUser(long id);

        #endregion

        #region UserStory

        public APIResponse<UserStory> GetUserStory(long id);
        public APIResponse<Pagination<UserStory>> GetUserStoryList(UserStoryFilter filter);
        public APIResponse<UserStory> CreateUserStory(UserStory model);
        public APIResponse<UserStory> UpdateUserStory(UserStory model);
        public APIResponse<string> DeleteUserStory(long id);

        #endregion
    }
}
