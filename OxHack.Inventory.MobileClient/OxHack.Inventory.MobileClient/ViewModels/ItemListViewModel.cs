using OxHack.Inventory.ApiClient;
using OxHack.Inventory.ApiClient.Models;
using OxHack.Inventory.MobileClient.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;
using OxHack.Inventory.MobileClient.Services;
using Prism.Commands;

namespace OxHack.Inventory.MobileClient.ViewModels
{
	public class ItemListViewModel : PageViewModelBase
	{
		private readonly InventoryClient inventoryClient;
		private Func<InventoryClient, Task<IEnumerable<Item>>> itemGetter;
		private readonly MessageService messageService;

		public ItemListViewModel(INavigation navigation, InventoryClient inventoryClient, MessageService messageService, string title, Func<InventoryClient, Task<IEnumerable<Item>>> itemGetter)
			: base(navigation)
		{
			this.inventoryClient = inventoryClient;
			this.messageService = messageService;
			this.Title = title;

			this.Items = new ObservableCollection<Item>();

			this.itemGetter = itemGetter;

			this.RefreshCommand = new DelegateCommand(async () => await this.LoadItems());
		}

		public async Task LoadItems()
		{
			try
			{
				var items = await this.itemGetter(this.inventoryClient);

				this.Items = new ObservableCollection<Item>(items);
				base.OnPropertyChanged(nameof(this.Items));
			}
			catch
			{
				// TODO: log error
			}
		}

		public void NavigateToSelectedItem()
		{
			var viewModel =
				new ItemDetailsViewModel(
					this.Navigation,
					this.inventoryClient,
					this.messageService,
					this.SelectedItem.Id);

			var forget = this.Navigation.PushAsync(new ItemDetailsPage(viewModel));
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

		public DelegateCommand RefreshCommand
		{
			get;
		}
	}
}