using OxHack.Inventory.WebClient.Models;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace OxHack.Inventory.MobileClient.ViewModels
{
	public class ItemDetailsViewModel : PageViewModelBase
	{
		private readonly Item model;

		public ItemDetailsViewModel(INavigation navigation, Item model)
		: base(navigation)
		{
			this.model = model;
		}

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