using OxHack.Inventory.ApiClient;
using OxHack.Inventory.ApiClient.Models;
using OxHack.Inventory.MobileClient.Views;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace OxHack.Inventory.MobileClient.ViewModels
{
	public class ItemListViewModel : PageViewModelBase
	{
		private readonly InventoryClient inventoryClient;

		public ItemListViewModel(INavigation navigation, InventoryClient inventoryClient, string title, Task<IEnumerable<Item>> itemsGetterTask)
			: base(navigation)
		{
			this.inventoryClient = inventoryClient;
			this.Title = title;

			itemsGetterTask.ContinueWith(items =>
			{
				if (items.Exception == null)
				{
					this.Items = new ObservableCollection<Item>(items.Result);
					base.OnPropertyChanged(nameof(this.Items));
				}
			});

			this.Items = new ObservableCollection<Item>();
		}

		public async Task NavigateToSelectedItem()
		{
			var target = await this.inventoryClient.GetItemByIdAsync(this.SelectedItem.Id);
			if (target != null)
			{
				var viewModel =
					new ItemDetailsViewModel(
						this.Navigation,
						this.inventoryClient,
						target);

				await this.Navigation.PushAsync(new ItemDetailsPage(viewModel));
			}
		}

		public ObservableCollection<Item> Items
		{
			get;
			private set;
		}

		public Item SelectedItem
		{
			get;
			set;
		}

		public string Title
		{
			get;
		}
	}
}