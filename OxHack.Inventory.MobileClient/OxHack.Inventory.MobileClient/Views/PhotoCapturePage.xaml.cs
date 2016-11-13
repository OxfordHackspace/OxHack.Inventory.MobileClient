using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OxHack.Inventory.MobileClient.ViewModels;
using Xamarin.Forms;
using System.ComponentModel;
using Prism.Commands;
using System.Net.Http;

namespace OxHack.Inventory.MobileClient.Views
{
	public partial class PhotoCapturePage : ContentPage
	{
		public PhotoCapturePage(Action<byte[]> onCapture)
		{
			this.InitializeComponent();

			this.CaptureCommand = new DelegateCommand(async () =>
			{
				if (await DisplayAlert("Let's pretend...", "... you just took a picture.", "OK", "NOPE"))
				{
					byte[] photoData = null;

					using (var client = new HttpClient())
					{
						photoData = await client.GetByteArrayAsync("http://lorempixel.com/400/400/");
					}
						
					var navigation = this.Navigation.PopModalAsync();

					onCapture(photoData);

					await navigation;
				}
			});

			this.BindingContext = this;
		}

		public DelegateCommand CaptureCommand
		{
			get;
		}
	}
}