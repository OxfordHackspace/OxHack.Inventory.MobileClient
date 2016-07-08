using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OxHack.Inventory.MobileClient.ViewModels;
using Xamarin.Forms;
using OxHack.Inventory.WebClient.Models;

namespace OxHack.Inventory.MobileClient.Views
{
	public partial class CategoryListPage : ContentPage
	{
		public CategoryListPage(CategoryListViewModel viewModel)
		{
			this.InitializeComponent();

			this.BindingContext = viewModel;

			this.categoryList.ItemTapped += (s, e) => viewModel.NavigateToSelectedCategory();
		}
	}
}
