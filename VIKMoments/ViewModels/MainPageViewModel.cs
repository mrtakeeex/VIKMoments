using Template10.Mvvm;
using System;
using System.Threading.Tasks;
using Template10.Services.NavigationService;
using VIKMoments.Models;
using VIKMoments.Views;
using System.Collections.ObjectModel;
using VIKMoments.Services.InstagramApiServices;
using Windows.UI.Xaml.Controls;
using VIKMoments.Views;
using Windows.UI.Popups;
using Template10.Behaviors;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Collections;

namespace VIKMoments.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        public ObservableCollection<Follower> FollowedList;
        public ObservableCollection<Follower> FollowersList;
        public ObservableCollection<UserMedia> UserMedia;
        public ObservableCollection<UserMedia> SelfMedia;
        public ObservableCollection<SearchedUser> SearchResult;
        public Profile _ProfileData;
        public Profile ProfileData
        {
            get { return _ProfileData; }
            set { Set(ref _ProfileData, value); }
        }
        public static string token { get; set; }

        public static bool loggedIn;

        private readonly IInstagramApiService _instagramApiService;
        public MainPageViewModel(IInstagramApiService instagramApiService)
        {
            _instagramApiService = instagramApiService;

            loggedIn = false;
            FollowersList = new ObservableCollection<Follower>();
            FollowedList = new ObservableCollection<Follower>();
            UserMedia = new ObservableCollection<UserMedia>();
            SelfMedia = new ObservableCollection<UserMedia>();
            SearchResult = new ObservableCollection<SearchedUser>();
    }

        public static async void login()
        {
            String _callbackUriString = "https://bmecookbook.azurewebsites.net/";
            Uri _callbackUri = new Uri(_callbackUriString);

            string startURL = "https://api.instagram.com/oauth/authorize/?client_id=18ff800b4997466ea46d8ce12920559c&redirect_uri="
                + Uri.EscapeDataString(_callbackUriString) + "&response_type=token" + "&scope=basic+public_content+follower_list+comments+relationships+likes";

            System.Uri startURI = new System.Uri(startURL);

            string result;

            try
            {
                var webAuthenticationResult =
                    await Windows.Security.Authentication.Web.WebAuthenticationBroker.AuthenticateAsync(
                    Windows.Security.Authentication.Web.WebAuthenticationOptions.None,
                    startURI, _callbackUri);

                switch (webAuthenticationResult.ResponseStatus)
                {
                    case Windows.Security.Authentication.Web.WebAuthenticationStatus.Success:
                        // Successful authentication. 
                        result = webAuthenticationResult.ResponseData.ToString();
                        token = result.Split('=')[1];
                        loggedIn = true;
                        break;
                    case Windows.Security.Authentication.Web.WebAuthenticationStatus.ErrorHttp:
                        // HTTP error. 
                        result = webAuthenticationResult.ResponseErrorDetail.ToString();
                        loggedIn = false;
                        break;
                    default:
                        // Other error.
                        result = webAuthenticationResult.ResponseData.ToString();
                        loggedIn = false;
                        break;
                }
            }
            catch (Exception ex)
            {
                // Authentication failed. Handle parameter, SSL/TLS, and Network Unavailable errors here. 
                result = ex.Message;
            }
        }

        public static void logout(object sender, object e)
        {
            token = null;
            loggedIn = false;

            MessageDialogAction msg = new MessageDialogAction();
            msg.Title = "You successfully logged out!";
            msg.Content = "Please login if you want to use the application!";
            msg.Execute(sender, e);
        }

        public async Task getMyProfile()
        {
            ProfileData = await _instagramApiService.GetMyProfileAsnyc(token);
        }

        public async Task getSelfMedia()
        {
            SelfMedia.Clear();
            SelfMedia = await _instagramApiService.GetSelfMediaAsnyc(token);
        }

        public async Task getSearchedResults(string keyword)
        {
            SearchResult.Clear();
            SearchResult = await _instagramApiService.GetSearchedUsersAsnyc(token, keyword);
        }

        public async Task getUserMedia()
        {
            UserMedia.Clear();
            var TemporaryUserMedia = new ObservableCollection<UserMedia>();

            if(FollowedList.Count < 1)
            {
                return;
            }
            // For every user
            foreach(var foll in FollowedList)
            {
                // Get users media
                var userMediaData = await _instagramApiService.GetUserMediaAsnyc(token, foll.id);

                // Iterate on each users media
                foreach(var iter in userMediaData)
                {
                    // Add media
                    TemporaryUserMedia.Add(iter);
                }
            }

            // Order by created_time
            var UserMediaList = TemporaryUserMedia.OrderByDescending(x => x.created_time).ToList();
            // Copy list into observablecollection
            foreach (var iter in UserMediaList)
            {
                UserMedia.Add(iter);
            }
        }
        public async Task getFollowed()
        {
            FollowedList.Clear();
            FollowedList = await _instagramApiService.GetFollowedAsnyc(token);
        }

        public async Task getFollowers()
        {
            FollowersList.Clear();
            FollowersList = await _instagramApiService.GetFollowersAsnyc(token);   
        }

        public async void Details(object sender, ItemClickEventArgs args)
        {
            if (!loggedIn)
            {
                return;
            }
            var follower = (Follower)args.ClickedItem;
            ProfileData = await _instagramApiService.GetUserProfileAsnyc(token, follower.id);

            ContentDialog dialog = new UserProfile_Dialog(this);
            await dialog.ShowAsync();
        }

        public async void DetailsWithSearch(object sender, ItemClickEventArgs args)
        {
            if (!loggedIn)
            {
                return;
            }
            var follower = (SearchedUser)args.ClickedItem;
            ProfileData = await _instagramApiService.GetUserProfileAsnyc(token, follower.id);

            ContentDialog dialog = new UserProfile_Dialog(this);
            await dialog.ShowAsync();
        }
    }
}

