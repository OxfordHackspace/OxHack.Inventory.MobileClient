using OxHack.Inventory.WebClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace OxHack.Inventory.MobileClient.ViewModels
{
    public class MainMenuViewModel
    {
        private readonly InventoryClient inventoryClient;

        public MainMenuViewModel(INavigation navigation, InventoryClient inventoryClient)
        {
            this.CategoriesViewModel = new CategoriesViewModel(navigation, inventoryClient);
        }

        public CategoriesViewModel CategoriesViewModel
        {
            get;
        }

        internal async Task InitializeAsync()
        {
            await Task.WhenAll(
                this.CategoriesViewModel.InitializeAsync()/*,
                this.OtherViewModel.InitializeAsync()*/
                );
        }
    }
}
