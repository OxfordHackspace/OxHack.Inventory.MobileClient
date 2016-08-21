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
	public partial class ItemDetailsPage : ContentPage
	{
		public ItemDetailsPage(ItemDetailsViewModel viewModel)
		{
			this.InitializeComponent();
			this.BindingContext = viewModel;

			// Hacko Pastorius!
			foreach (var photo in viewModel.Photos)
			{
				var image = new Image()
				{
					Source = photo,
				};

				var command = new DelegateCommand(() => Device.OpenUri(photo));

				image.GestureRecognizers.Add(
					new TapGestureRecognizer()
					{
						NumberOfTapsRequired = 2,
						Command = command
					});

				this.photoLayout.Children.Add(image);
			}
		}
	}
}
