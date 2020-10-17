using Prism;
using Prism.Ioc;
using Church.Prism.ViewModels;
using Church.Prism.Views;
using Xamarin.Essentials.Interfaces;
using Xamarin.Essentials.Implementation;
using Xamarin.Forms;
using Syncfusion.Licensing;
using Church.Common.Services;

namespace Church.Prism
{
    public partial class App
    {
        public App(IPlatformInitializer initializer)
            : base(initializer)
        {
        }

        protected override async void OnInitialized()
        {
            SyncfusionLicenseProvider.RegisterLicense("MzM2OTg1QDMxMzgyZTMzMmUzMGQxYjlGNTlGMjljMnVlNTJENThvQlFvY2FIQjB0YkZ2RmJyMTVzTFRCZlE9");
            InitializeComponent();

          // await NavigationService.NavigateAsync("NavigationPage/MainPage");
            await NavigationService.NavigateAsync($"NavigationPage/MembersPage");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IAppInfo, AppInfoImplementation>();
            containerRegistry.Register<IApiService, ApiService>();
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<MainPage, MainPageViewModel>();
            containerRegistry.RegisterForNavigation<MembersPage, MembersPageViewModel>();
            containerRegistry.RegisterForNavigation<MemberDetailPage, MemberDetailPageViewModel>();
        }
    }
}
