using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace OxHack.Inventory.MobileClient.Views
{
    public partial class SplashPage : ContentPage
    {
        public SplashPage()
        {
            InitializeComponent();
        }

        internal async Task BeginAnimation()
        {
            await this.pcControl.BeginAnimationAsync();
        }

		protected override void OnAppearing()
		{
			base.OnAppearing();

			NavigationPage.SetHasNavigationBar(this, false);
		}
	}
}
