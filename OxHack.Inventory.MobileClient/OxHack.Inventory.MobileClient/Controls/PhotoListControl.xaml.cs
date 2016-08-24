using OxHack.Inventory.MobileClient.Views;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using Xamarin.Forms;

namespace OxHack.Inventory.MobileClient.Controls
{
	public partial class PhotoListControl : ScrollView
	{
		public PhotoListControl()
		{
			this.InitializeComponent();
		}

		private void PrepareImages(IEnumerable<Uri> photos)
		{
			this.photoLayout.Children.Clear();

			foreach (var photo in photos)
			{
				var image = new Image()
				{
					Source = photo,
				};

				image.GestureRecognizers.Add(
					new TapGestureRecognizer()
					{
						NumberOfTapsRequired = 1,
						Command = new DelegateCommand(() => this.Navigation.PushModalAsync(
							new PhotoActionsPage(photo, () =>
								{
									var copy = this.Photos.ToList();
									copy.Remove(photo);
									this.Photos = copy;
									this.PrepareImages(this.Photos);
								})))
					});

			this.photoLayout.Children.Add(image);
		}
	}

	public List<Uri> Photos
	{
		get
		{
			return (List<Uri>)this.GetValue(PhotoListControl.PhotosProperty);
		}
		set
		{
			this.SetValue(PhotoListControl.PhotosProperty, value);
		}
	}

	public static readonly BindableProperty PhotosProperty =
		BindableProperty.Create(nameof(Photos), typeof(List<Uri>), typeof(PhotoListControl), new List<Uri>(), propertyChanged: OnPhotosChanged);

	private static void OnPhotosChanged(BindableObject bindable, object oldValue, object newValue)
	{
		var target = (PhotoListControl)bindable;

		var newPhotos = newValue as List<Uri>;
		if (newPhotos != null)
		{
			target.PrepareImages(newPhotos);
		}
	}
}
}
