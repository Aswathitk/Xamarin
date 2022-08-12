using MyCoffeeApp.Helpers;
using MyCoffeeApp.Services;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace MyCoffeeApp
{
    public partial class App : Application
    {

        public App()
        {
            TheTheme.SetTheme();
            InitializeComponent();
            MainPage = new AppShell();
          //  DependencyService.Register<ICoffeeService,CoffeeService>();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
            TheTheme.SetTheme();
            RequestedThemeChanged -= App_RequestedThemeChanged;
        }

        protected override void OnResume()
        {
            TheTheme.SetTheme();
            RequestedThemeChanged += App_RequestedThemeChanged;
        }

        private void App_RequestedThemeChanged(object sender, AppThemeChangedEventArgs e)
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                TheTheme.SetTheme();
            });
        }
    }
}
