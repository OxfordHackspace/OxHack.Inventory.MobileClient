using OxHack.Inventory.MobileClient.ViewModels;
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
        private readonly CategoriesPage categoriesPage;
        private readonly INavigation navigation;

        public MainMenuPage(MainMenuViewModel viewModel, INavigation navigation)
        {
            this.InitializeComponent();

            this.BindingContext = viewModel;
            this.navigation = navigation;

            this.categoriesPage = new CategoriesPage(viewModel.CategoriesViewModel);
        }

        public void OnBrowseByCategory(object sender, EventArgs args)
        {
            this.navigation.PushAsync(this.categoriesPage);
        }
    }
}
