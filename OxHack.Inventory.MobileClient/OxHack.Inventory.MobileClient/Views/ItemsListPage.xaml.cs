using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OxHack.Inventory.MobileClient.ViewModels;
using Xamarin.Forms;

namespace OxHack.Inventory.MobileClient.Views
{
    public partial class ItemsListPage : ContentPage
    {
        public ItemsListPage(ItemsListViewModel viewModel)
        {
            InitializeComponent();
            this.BindingContext = viewModel;
		}
    }
}
