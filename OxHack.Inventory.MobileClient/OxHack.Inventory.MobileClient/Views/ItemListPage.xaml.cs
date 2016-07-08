using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OxHack.Inventory.MobileClient.ViewModels;
using Xamarin.Forms;

namespace OxHack.Inventory.MobileClient.Views
{
    public partial class ItemListPage : ContentPage
    {
        public ItemListPage(ItemListViewModel viewModel)
        {
            this.InitializeComponent();

            this.BindingContext = viewModel;

			this.itemList.ItemTapped += (s, e) => viewModel.NavigateToSelectedItem();
		}
	}
}
