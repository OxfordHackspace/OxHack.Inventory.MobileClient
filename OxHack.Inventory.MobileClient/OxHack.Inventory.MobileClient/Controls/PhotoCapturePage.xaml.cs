using Plugin.Media;
using Plugin.Media.Abstractions;
using Prism.Commands;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace OxHack.Inventory.MobileClient.Controls
{
    public partial class PhotoCapturePage : ContentPage
    {
        public PhotoCapturePage(Action<byte[]> onCapture)
        {
            this.InitializeComponent();

            this.TakePhotoCommand = new DelegateCommand(async () =>
            {
                await this.TakePhoto(onCapture);
            });

            this.PickPhotoCommand = new DelegateCommand(async () =>
            {
                await this.PickPhoto(onCapture);
            });

            this.CloseCommand = new DelegateCommand(async () =>
            {
                await this.Navigation.PopModalAsync();
            });

            this.BindingContext = this;
        }

        private async Task TakePhoto(Action<byte[]> callback)
        {
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                await this.DisplayAlert("No Camera", ":( No camera available.", "OK");
                return;
            }

            using (var file = await CrossMedia.Current.TakePhotoAsync(
                new StoreCameraMediaOptions()
                {
                    Directory = "CameraSnaps",
                    PhotoSize = PhotoSize.Small,
                    Name = "snap.jpg"
                }))
            {
                this.ProcessData(callback, file);
            }

            await this.Navigation.PopModalAsync();
        }

        private async Task PickPhoto(Action<byte[]> callback)
        {
            await CrossMedia.Current.Initialize();

            using (var file = await CrossMedia.Current.PickPhotoAsync())
            {
                this.ProcessData(callback, file);
            }

            await this.Navigation.PopModalAsync();
        }

        private void ProcessData(Action<byte[]> callback, MediaFile file)
        {
            if (file != null && callback != null)
            {
                byte[] photoData;
                using (var stream = file.GetStream())
                {
                    photoData = new byte[stream.Length];
                    stream.Position = 0;
                    stream.Read(photoData, 0, (int)stream.Length);
                }

                callback(photoData);
            }
        }

        private void PocessImageFile(Action<byte[]> onCapture, MediaFile file)
        {

        }

        public DelegateCommand TakePhotoCommand
        {
            get;
        }

        public DelegateCommand PickPhotoCommand
        {
            get;
        }

        public DelegateCommand CloseCommand
        {
            get;
        }
    }
}