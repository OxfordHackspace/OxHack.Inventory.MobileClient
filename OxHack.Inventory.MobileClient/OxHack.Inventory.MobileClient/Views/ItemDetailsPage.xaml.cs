using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OxHack.Inventory.MobileClient.ViewModels;
using Xamarin.Forms;
using System.ComponentModel;

namespace OxHack.Inventory.MobileClient.Views
{
	public partial class ItemDetailsPage : ContentPage
	{
		public ItemDetailsPage(ItemDetailsViewModel viewModel)
		{
			this.InitializeComponent();
			this.BindingContext = viewModel;
		}
	}
}
