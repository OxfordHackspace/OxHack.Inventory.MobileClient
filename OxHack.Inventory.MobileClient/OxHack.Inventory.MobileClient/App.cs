using OxHack.Inventory.ApiClient;
using OxHack.Inventory.MobileClient.ViewModels;
using OxHack.Inventory.MobileClient.Views;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace OxHack.Inventory.MobileClient
{
	public class App : Application
    {
        private readonly Func<Task> beginAnimations;
        private readonly Func<Task> navigateToMainMenu;
        private readonly Func<Task> initializeViewModels;

        public App()
        {
            var splash = new SplashPage();
            this.MainPage = new NavigationPage(splash);

            var mainMenuViewModel = 
				new MainMenuViewModel(
					this.MainPage.Navigation, 
					new InventoryClient(AppConfig.CreateFromConfigFile().ApiUri),
					new Services.MessageService());

            var mainMenuPage = new MainMenuPage(mainMenuViewModel);

            this.initializeViewModels = async () => await mainMenuViewModel.InitializeAsync();
            this.beginAnimations = async () => await splash.BeginAnimation();
            this.navigateToMainMenu = async () =>
            {
                var navigating = this.MainPage.Navigation.PushAsync(mainMenuPage, true);
                this.MainPage.Navigation.RemovePage(splash);
                await navigating;
            };
        }

        protected override async void OnStart()
        {
            try
            {
                var init = this.initializeViewModels();
                if (!Debugger.IsAttached)
                {
                    await this.beginAnimations();
                    await Task.Delay(TimeSpan.FromSeconds(2));
                }
                await this.navigateToMainMenu();
                await init;
            }
            catch (Exception e)
            {
                await this.MainPage.DisplayAlert(
                    "Error during startup",
                    e.Message,
                    "Okie dokie");
            }
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
