using OxHack.Inventory.ApiClient;
using OxHack.Inventory.ApiClient.Models;
using OxHack.Inventory.MobileClient.Views;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.ComponentModel;
using Acr.UserDialogs;
using OxHack.Inventory.MobileClient.Services;

namespace OxHack.Inventory.MobileClient.ViewModels
{
	public class ItemDetailsViewModel : PageViewModelBase
	{
		private readonly InventoryClient inventoryClient;
		private readonly MessageService messageService;

		private Guid id;
		private string concurrencyId;
		private int version;
		private List<EditFieldViewModelBase> fields;

		public ItemDetailsViewModel(INavigation navigation, InventoryClient inventoryClient, MessageService messageService, Guid modelId)
		: base(navigation)
		{
			this.inventoryClient = inventoryClient;
			this.messageService = messageService;

			this.InitialiseEditFields();
			var forget = this.LoadModel(modelId);
		}

		private void InitialiseEditFields()
		{
			this.Name = new EditFieldViewModel<string>(async () => await this.SaveItemChangeAsync());
			this.Manufacturer = new EditFieldViewModel<string>(async () => await this.SaveItemChangeAsync());
			this.Model = new EditFieldViewModel<string>(async () => await this.SaveItemChangeAsync());
			this.Quantity = new EditFieldViewModel<int>(async () => await this.SaveItemChangeAsync());
			this.Category = new EditFieldViewModel<string>(async () => await this.SaveItemChangeAsync());
			this.Spec = new EditFieldViewModel<string>(async () => await this.SaveItemChangeAsync());
			this.Appearance = new EditFieldViewModel<string>(async () => await this.SaveItemChangeAsync());
			this.CurrentLocation = new EditFieldViewModel<string>(async () => await this.SaveItemChangeAsync());
			this.AssignedLocation = new EditFieldViewModel<string>(async () => await this.SaveItemChangeAsync());
			this.IsLoan = new EditFieldViewModel<bool>(async () => await this.SaveItemChangeAsync());
			this.Origin = new EditFieldViewModel<string>(async () => await this.SaveItemChangeAsync());
			this.AdditionalInformation = new EditFieldViewModel<string>(async () => await this.SaveItemChangeAsync());

			var fields = new List<EditFieldViewModelBase>();

			fields.Add(this.Name);
			fields.Add(this.Manufacturer);
			fields.Add(this.Model);
			fields.Add(this.Quantity);
			fields.Add(this.Category);
			fields.Add(this.Spec);
			fields.Add(this.Appearance);
			fields.Add(this.CurrentLocation);
			fields.Add(this.AssignedLocation);
			fields.Add(this.IsLoan);
			fields.Add(this.Origin);
			fields.Add(this.AdditionalInformation);

			foreach (var field in fields)
			{
				field.PropertyChanged += this.OnFieldPropertyChanged;
			}

			this.fields = fields;
		}

		private void OnFieldPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			if (e.PropertyName == nameof(EditFieldViewModel<Object>.IsInEditMode))
			{
				var isAnyInEditMode = this.fields.Any(item => item.IsInEditMode);

				foreach (var field in this.fields)
				{
					field.IsEditEnabled = !isAnyInEditMode;
				}
			}
		}

		private async Task<bool> LoadModel(Guid modelId, bool onlyIfUpdated = false)
		{
			bool wasLoaded = false;

			var model = await this.inventoryClient.GetItemByIdAsync(modelId);

			Func<bool> proceed =
				() =>
					onlyIfUpdated
						? this.version < model.Version
						: true;

			if (model != null && proceed())
			{
				this.id = model.Id;
				this.concurrencyId = model.ConcurrencyId;
				this.version = model.Version;

				this.Name.Value = model.Name;
				this.Manufacturer.Value = model.Manufacturer;
				this.Model.Value = model.Model;
				this.Quantity.Value = model.Quantity;
				this.Category.Value = model.Category;
				this.Spec.Value = model.Spec;
				this.Appearance.Value = model.Appearance;
				this.CurrentLocation.Value = model.CurrentLocation;
				this.AssignedLocation.Value = model.AssignedLocation;
				this.IsLoan.Value = model.IsLoan;
				this.Origin.Value = model.Origin;
				this.AdditionalInformation.Value = model.AdditionalInformation;

				this.Photos = model.Photos.ToList();
				this.OnPropertyChanged(nameof(this.Photos));

				wasLoaded = true;
			}

			return wasLoaded;
		}

		private Item CopyToModel()
		{
			var model = new Item(
				this.id,
				this.version,
				this.AdditionalInformation.Value,
				this.Appearance.Value,
				this.AssignedLocation.Value,
				this.Category.Value,
				this.CurrentLocation.Value,
				this.IsLoan.Value,
				this.Manufacturer.Value,
				this.Model.Value,
				this.Name.Value,
				this.Origin.Value,
				this.Quantity.Value,
				this.Spec.Value,
				this.Photos.ToList(),
				this.concurrencyId);

			return model;
		}

		private async Task SaveItemChangeAsync()
		{
			// Eventually, when OData is implemented, send over just the Delta using PATCH.
			// For now we send over the whole entity.
			await this.inventoryClient.SaveItemAsync(this.CopyToModel());
			await this.ReloadAfterSave();
		}

		private async Task SavePhotoAdditionAsync(byte[] photoData)
		{
			//this.messageService.ShowToast("Uploading photo.  Please wait...");
			await this.inventoryClient.AddPhotoToItem(this.id, this.concurrencyId, photoData);
			await this.ReloadAfterSave();
		}

		private async Task SavePhotoRemovalAsync(Uri removed)
		{
			var removedPhoto = removed.Segments.Last();

			await this.inventoryClient.RemovePhotoFromItem(this.id, this.concurrencyId, removedPhoto);
			await this.ReloadAfterSave();
		}

		private async Task ReloadAfterSave()
		{
			// TODO: Start an animation here.
			var tryCount = 3;
			for (int i = 1; i <= tryCount; i++)
			{
				await Task.Delay(500 * i);

				if (await this.LoadModel(this.id, onlyIfUpdated: true))
				{
					break;
				}

				if (i == tryCount)
				{
					// TODO: Show popup saying something to the effect of "Could not retrieve updated version of Item."
					await this.Navigation.PopAsync();
				}
			}
			// TODO: End the animation here.
		}

		public EditFieldViewModel<string> Name
		{
			get; set;
		}

		public EditFieldViewModel<string> Manufacturer
		{
			get; set;
		}

		public EditFieldViewModel<string> Model
		{
			get; set;
		}

		public EditFieldViewModel<int> Quantity
		{
			get; set;
		}

		public EditFieldViewModel<string> Category
		{
			get; set;
		}

		public EditFieldViewModel<string> Spec
		{
			get; set;
		}

		public EditFieldViewModel<string> Appearance
		{
			get; set;
		}

		public EditFieldViewModel<string> AssignedLocation
		{
			get; set;
		}

		public EditFieldViewModel<string> CurrentLocation
		{
			get; set;
		}

		public EditFieldViewModel<bool> IsLoan
		{
			get; set;
		}

		public EditFieldViewModel<string> Origin
		{
			get; set;
		}

		public EditFieldViewModel<string> AdditionalInformation
		{
			get; set;
		}

		public List<Uri> Photos
		{
			get; set;
		}

		public DelegateCommand<byte[]> AddPhotoCommand
			=> new DelegateCommand<byte[]>(async photoData => await this.SavePhotoAdditionAsync(photoData));

		public DelegateCommand<Uri> RemovePhotoCommand
			=> new DelegateCommand<Uri>(async photoUri => await this.SavePhotoRemovalAsync(photoUri));
	}
}