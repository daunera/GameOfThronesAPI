using System;
using System.Threading.Tasks;
using Template10.Services.NavigationService;
using Windows.ApplicationModel.Activation;
using Windows.ApplicationModel.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace GameOfThronesAPI
{

    [Bindable]
    sealed partial class App : Template10.Common.BootStrapper
    {
        private static int apiCalls;

        public static int ApiCalls
        {
            get { return apiCalls; }
            set { apiCalls = value; }
        }

        public App()
        {
            InitializeComponent();
            RequestedTheme = Windows.UI.Xaml.ApplicationTheme.Dark;
            ApiCalls = 0;
        }

        public override async Task OnStartAsync(StartKind startKind, IActivatedEventArgs args)
        {
            await NavigationService.NavigateAsync(typeof(Views.MainPage));
        }

        public override Task OnInitializeAsync(IActivatedEventArgs args)
        {
            var nav = NavigationServiceFactory(BackButton.Attach, ExistingContent.Include);
            Window.Current.Content = new Views.Shell((NavigationService)nav);
            return Task.FromResult<object>(null);
        }
    }
}
