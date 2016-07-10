using OxHack.Inventory.ApiClient;
using OxHack.Inventory.MobileClient.Views;
using Prism.Commands;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace OxHack.Inventory.MobileClient.ViewModels
{
	public class CategoryListViewModel : PageViewModelBase
	{
		private InventoryClient inventoryClient;

		public CategoryListViewModel(INavigation navigation, InventoryClient inventoryClient)
			: base(navigation)
		{
			this.inventoryClient = inventoryClient;

			this.Categories = new ObservableCollection<string>();

			this.RefreshCommand = new DelegateCommand(async () => await this.RefreshList());
		}

		internal async Task InitializeAsync()
		{
			await this.RefreshList();
		}

		private async Task RefreshList()
		{
			var categories = await this.inventoryClient.GetAllCategoriesAsync();
			this.Categories = new ObservableCollection<string>(categories);
			base.OnPropertyChanged(nameof(this.Categories));
		}

		public void NavigateToSelectedCategory()
		{
			var target = this.SelectedCategory;
			if (target != null)
			{
				var viewModel =
					new ItemListViewModel(
						this.Navigation,
						this.inventoryClient,
						$"{target} Category",
						this.inventoryClient.GetItemsInCategoryAsync(target));

				this.Navigation.PushAsync(new ItemListPage(viewModel));
			}
		}

		public ObservableCollection<string> Categories
		{
			get;
			private set;
		}

		public string SelectedCategory
		{
			get;
			set;
		}
		public DelegateCommand RefreshCommand
		{
			get;
			private set;
		}
	}
}