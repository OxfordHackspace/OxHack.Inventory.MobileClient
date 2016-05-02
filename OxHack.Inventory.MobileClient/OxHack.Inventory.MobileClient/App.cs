using OxHack.Inventory.MobileClient.Views;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace OxHack.Inventory.MobileClient
{
	public class App : Application
	{
		private readonly Func<Task> beginAnimations;
		private readonly Func<Task> navigateToMainMenu;

		public App()
		{
			var splash = new SplashPage();
			var navigationSplash = new NavigationPage(splash);

			this.MainPage = navigationSplash;

			this.beginAnimations = () => splash.BeginAnimation();
			this.navigateToMainMenu = async () =>
			{
				var navigation = splash.Navigation.PushAsync(new MainMenuPage(), true);
				splash.Navigation.RemovePage(splash);
				await navigation;
			};
		}

		public void HackStart()
		{
			this.OnStart();
		}

		protected override async void OnStart()
		{
			await this.beginAnimations();
			await Task.Delay(TimeSpan.FromSeconds(2));
			await this.navigateToMainMenu();
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
