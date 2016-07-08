using OxHack.Inventory.MobileClient.Views;
using OxHack.Inventory.WebClient.Models;
using Prism.Mvvm;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace OxHack.Inventory.MobileClient.ViewModels
{
	public class ItemListViewModel : PageViewModelBase
	{
		public ItemListViewModel(INavigation navigation, string title, Task<IEnumerable<Item>> itemsGetter)
			: base(navigation)
		{
			this.Title = title;

			itemsGetter.ContinueWith(items =>
			{
				if (items.Exception == null)
				{
					this.Items = new ObservableCollection<Item>(items.Result);
					base.OnPropertyChanged(nameof(this.Items));
				}
			});

			this.Items = new ObservableCollection<Item>();
		}

		public void NavigateToSelectedItem()
		{
			var target = this.SelectedItem;
			if (target != null)
			{
				var viewModel =
					new ItemDetailsViewModel(
						this.Navigation,
						target);

				this.Navigation.PushAsync(new ItemDetailsPage(viewModel));
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