using OxHack.Inventory.ApiClient;
using OxHack.Inventory.MobileClient.Views;
using Prism.Commands;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace OxHack.Inventory.MobileClient.ViewModels
{
	public class MainMenuViewModel : PageViewModelBase
	{
        private readonly CategoryListViewModel categoriesViewModel;
        private readonly AddItemPage addItemPage;

        public MainMenuViewModel(INavigation navigation, InventoryClient inventoryClient)
		: base(navigation)
		{
			this.categoriesViewModel = new CategoryListViewModel(navigation, inventoryClient);
			this.BrowseByCategoryCommand = new DelegateCommand(() => this.Navigation.PushAsync(new CategoryListPage(this.categoriesViewModel)));

            this.addItemPage = new AddItemPage(new AddItemViewModel(navigation, inventoryClient));
			this.AddItemCommand = new DelegateCommand(() => this.Navigation.PushModalAsync(this.addItemPage));
		}

		internal async Task InitializeAsync()
		{
			await Task.WhenAll(
				this.categoriesViewModel.InitializeAsync()/*,
                this.OtherViewModel.InitializeAsync()*/
				);
		}

		public DelegateCommand BrowseByCategoryCommand
		{
			get;
		}

		public DelegateCommand AddItemCommand
		{
			get;
		}
	}
}
