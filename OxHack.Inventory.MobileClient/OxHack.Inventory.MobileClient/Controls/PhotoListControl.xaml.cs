using Prism.Commands;
using System;
using System.Collections.Generic;
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
				var image = new Frame()
				{
					Content = new Image()
					{
						Source = photo,
					},
					BackgroundColor = Color.Accent.MultiplyAlpha(1d / 8),
					Padding = new Thickness(10)
				};

				image.GestureRecognizers.Add(
					new TapGestureRecognizer()
					{
						NumberOfTapsRequired = 1,
						Command = new DelegateCommand(() => this.Navigation.PushModalAsync(
							new PhotoActionsPage(photo, () =>
								{
									this.Photos.Remove(photo);
									this.PhotoRemovedCommand?.Execute(photo);
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
			BindableProperty.Create(nameof(Photos), typeof(List<Uri>), typeof(PhotoListControl), new List<Uri>(),
				propertyChanged: OnPhotosChanged);

		private static void OnPhotosChanged(BindableObject bindable, object oldValue, object newValue)
		{
			var target = (PhotoListControl)bindable;

			var newPhotos = newValue as List<Uri>;
			if (newPhotos != null)
			{
				target.PrepareImages(newPhotos);
			}
		}

		public DelegateCommand<byte[]> PhotoAddedCommand
		{
			get
			{
				return (DelegateCommand<byte[]>)this.GetValue(PhotoListControl.PhotoAddedCommandProperty);
			}
			set
			{
				this.SetValue(PhotoListControl.PhotoAddedCommandProperty, value);
			}
		}

		public static readonly BindableProperty PhotoAddedCommandProperty =
			BindableProperty.Create(nameof(PhotoAddedCommand), typeof(DelegateCommand<byte[]>), typeof(PhotoListControl), null);

		public DelegateCommand<Uri> PhotoRemovedCommand
		{
			get
			{
				return (DelegateCommand<Uri>)this.GetValue(PhotoListControl.PhotoRemovedCommandProperty);
			}
			set
			{
				this.SetValue(PhotoListControl.PhotoRemovedCommandProperty, value);
			}
		}

		public static readonly BindableProperty PhotoRemovedCommandProperty =
			BindableProperty.Create(nameof(PhotoRemovedCommand), typeof(DelegateCommand<Uri>), typeof(PhotoListControl), null);

		public DelegateCommand OpenCameraCommand
			=> new DelegateCommand(
				() => this.Navigation.PushModalAsync(
					new PhotoCapturePage(photoData =>
					{
						this.PhotoAddedCommand?.Execute(photoData);
					})));
	}
}
