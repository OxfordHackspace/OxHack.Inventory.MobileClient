using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Plugin.Permissions;
using Acr.UserDialogs;
using HockeyApp.Android;
using HockeyApp.Android.Metrics;

namespace OxHack.Inventory.MobileClient.Droid
{
	[Activity(Label = "Inventory", Icon = "@drawable/icon", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
	public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsApplicationActivity
	{
		protected override void OnCreate(Bundle bundle)
		{
			base.OnCreate(bundle);

			CrashManager.Register(this);
			MetricsManager.Register(this.Application);

			global::Xamarin.Forms.Forms.Init(this, bundle);

			UserDialogs.Init(this);

			LoadApplication(new App());

			this.CheckForUpdates();
		}

		private void CheckForUpdates()
		{
			// Remove this for store builds!
			UpdateManager.Register(this);
		}

		private void UnregisterManagers()
		{
			UpdateManager.Unregister();
		}

		protected override void OnPause()
		{
			base.OnPause();

			this.UnregisterManagers();
		}

		protected override void OnDestroy()
		{
			base.OnDestroy();

			this.UnregisterManagers();
		}

		public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Permission[] grantResults)
		{
			PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
		}
	}
}

