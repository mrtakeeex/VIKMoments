using VIKMoments.Services.InstagramApiServices;
using VIKMoments.ViewModels;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using VIKMoments.Models;
using Template10.Behaviors;

namespace VIKMoments.Views
{
    public sealed partial class MainPage : Page
    {
        public MainPageViewModel ViewModel;
        //public MainPageViewModel ViewModel => (MainPageViewModel)DataContext;

        public MainPage()
        {
            InitializeComponent();
            NavigationCacheMode = Windows.UI.Xaml.Navigation.NavigationCacheMode.Enabled;            

            this.ViewModel = new MainPageViewModel(new InstagramApiService(MainPageViewModel.token));
            this.DataContext = ViewModel;
        }


        private async void rootPivot_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (MainPageViewModel.loggedIn)
            {
                switch (rootPivot.SelectedIndex)
                {
                    case 0:
                        // News feed
                        await ViewModel.getUserMedia();
                        newsFeedListView.ItemsSource = ViewModel.UserMedia;                       
                        break;
                    case 1:
                        // My profile
                        await ViewModel.getMyProfile();
                        await ViewModel.getSelfMedia();
                        selfMediaListView.ItemsSource = ViewModel.SelfMedia;
                        break;
                    case 2:
                        // Followed
                        await ViewModel.getFollowed();
                        followedListView.ItemsSource = ViewModel.FollowedList;
                        break;
                    case 3:
                        // Followers
                        await ViewModel.getFollowers();
                        followedByListView.ItemsSource = ViewModel.FollowersList;
                        break;
                    case 4:
                        // Search
                        break;
                }
            }
            else { }
        }

        private async void searchButton_Click(object sender, RoutedEventArgs e)
        {
            if (keywordField.Text.Length > 0)
                await ViewModel.getSearchedResults(keywordField.Text);
            else
                return;

            if(ViewModel.SearchResult.Count < 1)
            {
                MessageDialogAction msg = new MessageDialogAction();
                msg.Title = "Not found!";
                msg.Content = "There is no matching user with keyword \'" + keywordField.Text + "\'";
                msg.Execute(sender, e);
            } else
                searchListView.ItemsSource = ViewModel.SearchResult;
        }
    }
}
