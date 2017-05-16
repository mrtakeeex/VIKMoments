using System.ComponentModel;
using System.Linq;
using System;
using Template10.Common;
using Template10.Controls;
using Template10.Services.NavigationService;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Template10.Mvvm;
using VIKMoments.ViewModels;
using Windows.UI.Popups;
using Template10.Behaviors;

namespace VIKMoments.Views
{
    public sealed partial class Shell : Page
    {
        public static Shell Instance { get; set; }
        public static HamburgerMenu HamburgerMenu => Instance.MyHamburgerMenu;
        Services.SettingsServices.SettingsService _settings;

        public Shell()
        {
            Instance = this;
            InitializeComponent();
            _settings = Services.SettingsServices.SettingsService.Instance;

            MainPageViewModel.login();
            loginButtonText.Text = "Logout";
        }

        private void loginButton_Click(object sender, RoutedEventArgs e)
        {
            if (!MainPageViewModel.loggedIn)
            {
                // Login
                MainPageViewModel.login();
                MainPageViewModel.loggedIn = true;
                loginButtonText.Text = "Logout";
            }
            else
            {
                // Logout
                MainPageViewModel.logout(sender, e);
                loginButtonText.Text = "Login";
            }

        }
        public Shell(INavigationService navigationService) : this()
        {
            SetNavigationService(navigationService);
        }

        public void SetNavigationService(INavigationService navigationService)
        {
            MyHamburgerMenu.NavigationService = navigationService;
            HamburgerMenu.RefreshStyles(_settings.AppTheme, true);
            HamburgerMenu.IsFullScreen = _settings.IsFullScreen;
            HamburgerMenu.HamburgerButtonVisibility = _settings.ShowHamburgerButton ? Visibility.Visible : Visibility.Collapsed;
        }
    }
}

