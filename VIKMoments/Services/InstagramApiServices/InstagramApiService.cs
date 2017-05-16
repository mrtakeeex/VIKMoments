using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using VIKMoments.Models;

namespace VIKMoments.Services.InstagramApiServices
{
    public class InstagramApiService: IInstagramApiService
    {
        private string token;

        public InstagramApiService(string token)
        {
            this.token = token;
        }

        public async Task<ObservableCollection<SearchedUser>> GetSearchedUsersAsnyc(string token, string keyword)
        {
            string uri = "https://api.instagram.com/v1/users/search?q=" + keyword + "&access_token=" + token;
            var response = await GetRequestAsnyc<SearchResponse>(uri);
            return response.data;
        }

        public async Task<Profile> GetMyProfileAsnyc(string token)
        {
            string uri = "https://api.instagram.com/v1/users/self/?access_token=" + token;
            var response = await GetRequestAsnyc<ProfileResponse>(uri);
            return response.data;
        }

        public async Task<Profile> GetUserProfileAsnyc(string token, string userid)
        {
            string uri = "https://api.instagram.com/v1/users/" + userid + "/?access_token=" + token;
            var response = await GetRequestAsnyc<ProfileResponse>(uri);
            return response.data;
        }

        public async Task<ObservableCollection<Follower>> GetFollowedAsnyc(string token)
        {
            string uri = "https://api.instagram.com/v1/users/self/follows?access_token=" + token;
            var response = await GetRequestAsnyc<FollowedResponse>(uri);
            return response.data;
        }

        public async Task<ObservableCollection<Follower>> GetFollowersAsnyc(string token)
        {
            string uri = "https://api.instagram.com/v1/users/self/followed-by?access_token=" + token;
            var response = await GetRequestAsnyc<FollowedByResponse>(uri);
            return response.data;
        }

        public async Task<ObservableCollection<UserMedia>> GetSelfMediaAsnyc(string token)
        {
            string uri = "https://api.instagram.com/v1/users/self/media/recent/?access_token=" + token;
            var response = await GetRequestAsnyc<SelfMediaResponse>(uri);
            return response.data;
        }

        public async Task<ObservableCollection<UserMedia>> GetUserMediaAsnyc(string token, string userid)
        {
            string uri = "https://api.instagram.com/v1/users/"+ userid +"/media/recent/?access_token=" + token;
            var response = await GetRequestAsnyc<RecentMediaResponse>(uri);
            return response.data;
        }

        public async Task<T> GetRequestAsnyc<T>(string uri)
        {
            using (var client = new HttpClient())
            {
                var json = await client.GetStringAsync(new Uri(uri));
                T result = JsonConvert.DeserializeObject<T>(json);
                return result;
            }
        }
    }
}
