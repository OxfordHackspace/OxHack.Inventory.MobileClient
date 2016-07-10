using OxHack.Inventory.ApiClient;
using OxHack.Inventory.MobileClient.Views;
using Prism.Commands;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace OxHack.Inventory.MobileClient.ViewModels
{
	public class MainMenuViewModel : PageViewModelBase
	{
		public MainMenuViewModel(INavigation navigation, InventoryClient inventoryClient)
		: base(navigation)
		{
			this.CategoriesViewModel = new CategoryListViewModel(navigation, inventoryClient);

			this.BrowseByCategoryCommand = new DelegateCommand(() => this.Navigation.PushAsync(new CategoryListPage(this.CategoriesViewModel)));
		}

		public DelegateCommand BrowseByCategoryCommand
		{
			get;
			private set;
		}
		public CategoryListViewModel CategoriesViewModel
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
