using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OxHack.Inventory.MobileClient.ViewModels;
using Xamarin.Forms;
using System.ComponentModel;
using Prism.Commands;

namespace OxHack.Inventory.MobileClient.Views
{
	public partial class AddItemPage : ContentPage
	{
		public AddItemPage(AddItemViewModel viewModel)
		{
			this.InitializeComponent();
			this.BindingContext = viewModel;
		}
	}
}
