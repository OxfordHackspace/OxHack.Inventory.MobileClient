using OxHack.Inventory.ApiClient;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace OxHack.Inventory.MobileClient.ViewModels
{
	public class MainMenuViewModel : PageViewModelBase
	{
		private readonly InventoryClient inventoryClient;

		public MainMenuViewModel(INavigation navigation, InventoryClient inventoryClient)
		: base(navigation)
		{
			this.CategoriesViewModel = new CategoryListViewModel(navigation, inventoryClient);
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
