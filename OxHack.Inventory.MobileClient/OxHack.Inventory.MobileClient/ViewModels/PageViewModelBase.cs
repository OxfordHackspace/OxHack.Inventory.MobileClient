using OxHack.Inventory.WebClient.Models;
using Prism.Mvvm;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
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