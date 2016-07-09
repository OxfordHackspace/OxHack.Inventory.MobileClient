using Prism.Mvvm;
using Xamarin.Forms;

namespace OxHack.Inventory.MobileClient.ViewModels
{
	public abstract class PageViewModelBase : BindableBase
    {
		public PageViewModelBase(INavigation navigation)
        {
			this.Navigation = navigation;
		}

		protected INavigation Navigation
		{
			get;
		}
	}
}