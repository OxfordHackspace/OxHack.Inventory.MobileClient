using OxHack.Inventory.ApiClient;
using OxHack.Inventory.ApiClient.Models;
using OxHack.Inventory.MobileClient.Views;
using Prism.Commands;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace OxHack.Inventory.MobileClient.ViewModels
{
	public class ItemDetailsViewModel : PageViewModelBase
	{
		private readonly Item model;
		private readonly InventoryClient inventoryClient;
		private bool isEditing;

		public ItemDetailsViewModel(INavigation navigation, InventoryClient inventoryClient, Item model, bool isEditing = false)
		: base(navigation)
		{
			this.inventoryClient = inventoryClient;
			this.model = model;

			this.ToolBarItemCommand =
				new DelegateCommand(async () =>
				{
					if (!this.IsEditing)
					{
						var target = await this.inventoryClient.GetItemByIdAsync(this.model.Id);
						await this.Navigation.PushAsync(new ItemDetailsPage(new ItemDetailsViewModel(this.Navigation, this.inventoryClient, target, isEditing: true)));
					}
					else
					{
						await this.Navigation.PopAsync();
					}
				});

			this.IsEditing = isEditing;
		}

		public string Title
		{
			get
			{
				return
					this.IsEditing
						? "Editing Item Details"
						: "Item Details";
			}
		}

		public DelegateCommand ToolBarItemCommand
		{
			get;
			private set;
		}

		public string ToolBarItemCommandText
			=> this.IsEditing ? "Save" : "Edit";

		public bool IsEditing
		{
			get
			{
				return this.isEditing;
			}
			set
			{
				base.SetProperty(ref this.isEditing, value);
				base.OnPropertyChanged(nameof(this.IsNotEditing));
			}
		}

		public bool IsNotEditing
			=> !this.IsEditing;

		public string Name
			=> this.model.Name;

		public string Manufacturer
			=> this.model.Manufacturer;

		public string Model
			=> this.model.Model;

		public int Quantity
			=> this.model.Quantity;

		public string Category
			=> this.model.Category;

		public string Spec
			=> this.model.Spec;

		public string Appearance
			=> this.model.Appearance;

		public string AssignedLocation
			=> this.model.AssignedLocation;

		public string CurrentLocation
			=> this.model.CurrentLocation;

		public bool IsLoan
			=> this.model.IsLoan;

		public string Origin
			=> this.model.Origin;

		public string AdditionalInformation
			=> this.model.AdditionalInformation;

		public IEnumerable<Uri> Photos
			=> this.model.Photos;
	}
}