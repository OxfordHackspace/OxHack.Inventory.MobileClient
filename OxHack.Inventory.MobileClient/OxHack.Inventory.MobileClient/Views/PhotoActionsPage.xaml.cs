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
	public partial class PhotoActionsPage : ContentPage
	{
		public PhotoActionsPage(Uri photo, Action onRemove)
		{
			this.InitializeComponent();

			this.Photo = photo;

			this.OpenInBrowserCommand = new DelegateCommand(() => Device.OpenUri(this.Photo));
			this.CloseCommand = new DelegateCommand(() => this.Navigation.PopModalAsync());
			this.RemoveFromItemCommand = new DelegateCommand(async () =>
			{
				if (await DisplayAlert("Remove Photo from Item", "Are you sure?", "Yes", "No"))
				{
					var closing = this.CloseCommand.Execute();
					var delay = Task.Delay(TimeSpan.FromSeconds(1));

					await Task.WhenAll(closing, delay);
					onRemove();
				}
			});

			this.BindingContext = this;
		}

		public Uri Photo
		{
			get;
		}

		public DelegateCommand OpenInBrowserCommand
		{
			get;
		}

		public DelegateCommand RemoveFromItemCommand
		{
			get;
		}

		public DelegateCommand CloseCommand
		{
			get;
		}
	}
}
