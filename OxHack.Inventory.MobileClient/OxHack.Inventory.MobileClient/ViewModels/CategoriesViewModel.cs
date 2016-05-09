using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OxHack.Inventory.WebClient;
using System.Collections.ObjectModel;
using Prism.Mvvm;
using Xamarin.Forms;
using OxHack.Inventory.MobileClient.Views;

namespace OxHack.Inventory.MobileClient.ViewModels
{
    public class CategoriesViewModel : BindableBase
    {
        private readonly INavigation navigation;
        private InventoryClient inventoryClient;
        private string selectedCategory;

        public CategoriesViewModel(INavigation navigation, InventoryClient inventoryClient)
        {
            this.navigation = navigation;
            this.inventoryClient = inventoryClient;

            this.Categories = new ObservableCollection<string>();
        }

        internal async Task InitializeAsync()
        {
            var categories = await this.inventoryClient.GetAllCategoriesAsync();
            this.Categories = new ObservableCollection<string>(categories);
            base.OnPropertyChanged(nameof(this.Categories));
        }

        public ObservableCollection<string> Categories
        {
            get;
            private set;
        }

        public string SelectedCategory
        {
            get
            {
                return this.selectedCategory;
            }
            set
            {
                base.SetProperty(ref this.selectedCategory, value);

				if (value != null)
				{
                    var viewModel =
                        new ItemsListViewModel(
                            $"{value} Category",
                            this.inventoryClient.GetItemsInCategoryAsync(value));

                    this.navigation.PushAsync(new ItemsListPage(viewModel));

                    Task.Delay(500).ContinueWith((prev) => this.SelectedCategory = null);
				}
            }
        }
    }
}