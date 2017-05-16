using Windows.UI.Xaml;
using System.Threading.Tasks;
using VIKMoments.Services.SettingsServices;
using Windows.ApplicationModel.Activation;
using Template10.Controls;
using Template10.Common;
using Autofac;
using Windows.UI.Xaml.Data;
using System.ComponentModel;
using VIKMoments.Services.InstagramApiServices;
using VIKMoments.ViewModels;
using Template10.Services.NavigationService;
using Windows.UI.Xaml.Controls;
using VIKMoments.Views;

namespace VIKMoments
{
    /// Documentation on APIs used in this page:
    /// https://github.com/Windows-XAML/Template10/wiki

    [Bindable]
    sealed partial class App : BootStrapper
    {
        public App()
        {
            InitializeComponent();
            SplashFactory = (e) => new Views.Splash(e);

            #region app settings

            // some settings must be set in app.constructor
            var settings = SettingsService.Instance;
            RequestedTheme = settings.AppTheme;
            CacheMaxDuration = settings.CacheMaxDuration;
            ShowShellBackButton = settings.UseShellBackButton;
            AutoSuspendAllFrames = true;
            AutoRestoreAfterTerminated = true;
            AutoExtendExecutionSession = true;

            #endregion
        }

        public override UIElement CreateRootElement(IActivatedEventArgs e)
        {
            var service = NavigationServiceFactory(BackButton.Attach, ExistingContent.Exclude);
            return new ModalDialog
            {
                DisableBackButtonWhenModal = true,
                Content = new Views.Shell(service),
                ModalContent = new Views.Busy(),
            };
        }

        private Autofac.IContainer _container;
        private void ConfigureDependencies()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<InstagramApiService>().As<IInstagramApiService>().InstancePerLifetimeScope();
            builder.RegisterType<MainPageViewModel>().InstancePerDependency();

            _container = builder.Build();
        }

        //public override INavigable ResolveForPage(Page page, NavigationService navigationService)
        //{
        //    if (page is MainPage)
        //    {
        //        return _container.Resolve<MainPageViewModel>();
        //    }
        //    else
        //    {
        //        return base.ResolveForPage(page, navigationService);
        //    }
        //}

        public override async Task OnStartAsync(StartKind startKind, IActivatedEventArgs args)
        {
            ConfigureDependencies();
            await NavigationService.NavigateAsync(typeof(Views.MainPage));
        }
    }
}

