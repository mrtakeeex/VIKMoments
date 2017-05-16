using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VIKMoments.Models;

namespace VIKMoments.Services.InstagramApiServices
{
    public interface IInstagramApiService
    {
        Task<Profile> GetMyProfileAsnyc(string token);

        Task<Profile> GetUserProfileAsnyc(string token, string userid);

        Task<ObservableCollection<SearchedUser>> GetSearchedUsersAsnyc(string token, string keyword);

        Task<ObservableCollection<UserMedia>> GetSelfMediaAsnyc(string token);

        Task<ObservableCollection<UserMedia>> GetUserMediaAsnyc(string token, string userid);

        Task<ObservableCollection<Follower>> GetFollowedAsnyc(string token);

        Task<ObservableCollection<Follower>> GetFollowersAsnyc(string token);

        Task<T> GetRequestAsnyc<T>(string uri);
    }
}
