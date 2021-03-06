﻿using Acr.UserDialogs;
using OxHack.Inventory.ApiClient;
using OxHack.Inventory.ApiClient.Models;
using OxHack.Inventory.MobileClient.Services;
using OxHack.Inventory.MobileClient.Views;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace OxHack.Inventory.MobileClient.ViewModels
{
	public class AddItemViewModel : PageViewModelBase
	{
		private readonly InventoryClient inventoryClient;
		private readonly MessageService messageService;

		public AddItemViewModel(INavigation navigation, InventoryClient inventoryClient, MessageService messageService)
			: base(navigation)
		{
			this.inventoryClient = inventoryClient;
			this.messageService = messageService;

			this.Reset();
		}

		public string Name
		{
			get; set;
		}

		public string Manufacturer
		{
			get; set;
		}

		public string Model
		{
			get; set;
		}

		public int Quantity
		{
			get; set;
		}

		public string Category
		{
			get; set;
		}

		public string Spec
		{
			get; set;
		}

		public string Appearance
		{
			get; set;
		}

		public string AssignedLocation
		{
			get; set;
		}

		public string CurrentLocation
		{
			get; set;
		}

		public bool IsLoan
		{
			get; set;
		}

		public string Origin
		{
			get; set;
		}

		public string AdditionalInformation
		{
			get; set;
		}

		public List<Uri> Photos
		{
			get;
			private set;
		}

		private async Task SubmitNewItem()
		{
			var model = this.CopyToModel();

			bool createdSuccessfully = false;

			if (Validate(model))
			{
				try
				{
					await this.inventoryClient.CreateItemAsync(model);
					createdSuccessfully = true;
				}
				catch
				{
					this.messageService.ShowError("Item creation failed :|.  Sorry!");
				}
			}

			if (createdSuccessfully)
			{
				this.Reset();
				await this.Navigation.PopModalAsync();
				await this.Navigation.PushAsync(new ItemDetailsPage(new ItemDetailsViewModel(this.Navigation, this.inventoryClient, this.messageService, model.Id)));
			}
		}

		private bool Validate(Item model)
		{
			List<string> invalidFields = new List<string>();

			if (String.IsNullOrWhiteSpace(model.Name))
			{
				invalidFields.Add(nameof(model.Name));
			}

			if (String.IsNullOrWhiteSpace(model.Category))
			{
				invalidFields.Add(nameof(model.Category));
			}

			if (String.IsNullOrWhiteSpace(model.Appearance))
			{
				invalidFields.Add(nameof(model.Appearance));
			}

			if (String.IsNullOrWhiteSpace(model.AssignedLocation))
			{
				invalidFields.Add(nameof(model.AssignedLocation));
			}

			bool isValid;
			if (invalidFields.Any())
			{
				isValid = false;
				this.messageService.ShowError("Please fill in the following fields: " + String.Join(", ", invalidFields));
			}
			else
			{
				isValid = true;
			}

			return isValid;
		}

		private Item CopyToModel()
		{
			var model = new Item(
				Guid.NewGuid(),
				0,
				this.AdditionalInformation.Trim(),
				this.Appearance.Trim(),
				this.AssignedLocation.Trim(),
				this.Category.Trim(),
				this.CurrentLocation.Trim(),
				this.IsLoan,
				this.Manufacturer.Trim(),
				this.Model.Trim(),
				this.Name.Trim(),
				this.Origin.Trim(),
				this.Quantity,
				this.Spec.Trim(),
				this.Photos.ToList(),
				null);

			return model;
		}

		private void Reset()
		{
			this.AdditionalInformation = String.Empty;
			this.Appearance = String.Empty;
			this.AssignedLocation = String.Empty;
			this.Category = String.Empty;
			this.CurrentLocation = String.Empty;
			this.IsLoan = false;
			this.Manufacturer = String.Empty;
			this.Model = String.Empty;
			this.Name = String.Empty;
			this.Origin = String.Empty;
			this.Quantity = 1;
			this.Spec = String.Empty;
			this.Photos = new List<Uri>();

			this.OnPropertyChanged(nameof(this.AdditionalInformation));
			this.OnPropertyChanged(nameof(this.Appearance));
			this.OnPropertyChanged(nameof(this.AssignedLocation));
			this.OnPropertyChanged(nameof(this.Category));
			this.OnPropertyChanged(nameof(this.CurrentLocation));
			this.OnPropertyChanged(nameof(this.IsLoan));
			this.OnPropertyChanged(nameof(this.Manufacturer));
			this.OnPropertyChanged(nameof(this.Model));
			this.OnPropertyChanged(nameof(this.Name));
			this.OnPropertyChanged(nameof(this.Origin));
			this.OnPropertyChanged(nameof(this.Quantity));
			this.OnPropertyChanged(nameof(this.Spec));
			this.OnPropertyChanged(nameof(this.Photos));
		}

		public DelegateCommand CancelCommand
			=> new DelegateCommand(async () => await this.Navigation.PopModalAsync());

		public DelegateCommand SaveCommand
			=> new DelegateCommand(async () => await this.SubmitNewItem());

		public DelegateCommand<byte[]> AddPhotoCommand
			=> new DelegateCommand<byte[]>(
				async photoData =>
				{
					var photoUri = await this.inventoryClient.UploadPhoto(photoData);

					this.Photos.Add(photoUri);
					this.Photos = this.Photos.ToList(); // hack to get binding to work
					this.OnPropertyChanged(nameof(this.Photos));
				});

		public DelegateCommand<Uri> RemovePhotoCommand
			=> new DelegateCommand<Uri>(
				photoUri =>
				{
					this.Photos.Remove(photoUri);
					this.Photos = this.Photos.ToList(); // hack to get binding to work
					this.OnPropertyChanged(nameof(this.Photos));
				});
	}
}