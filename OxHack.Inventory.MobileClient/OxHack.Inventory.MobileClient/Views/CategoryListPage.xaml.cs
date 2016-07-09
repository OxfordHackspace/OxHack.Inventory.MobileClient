using OxHack.Inventory.MobileClient.ViewModels;
using Xamarin.Forms;

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
