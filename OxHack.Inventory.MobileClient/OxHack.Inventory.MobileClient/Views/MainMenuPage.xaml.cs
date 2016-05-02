using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace OxHack.Inventory.MobileClient.Views
{
    public partial class MainMenuPage : ContentPage
    {
        public MainMenuPage()
        {
            this.InitializeComponent();
		}

		public void OnBrowseByCategory(object sender, EventArgs args)
		{
			this.Navigation.PushAsync(new CategoriesPage());
		}
	}
}
