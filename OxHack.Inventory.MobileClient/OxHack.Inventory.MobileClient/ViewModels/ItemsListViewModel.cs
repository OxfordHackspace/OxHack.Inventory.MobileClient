using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OxHack.Inventory.WebClient;
using System.Collections.ObjectModel;
using Prism.Mvvm;
using OxHack.Inventory.WebClient.Models;

namespace OxHack.Inventory.MobileClient.ViewModels
{
    public class ItemsListViewModel : BindableBase
    {
        public ItemsListViewModel(string title, Task<IEnumerable<Item>> taskGetter)
        {
			this.Title = title;

            taskGetter.ContinueWith(items =>
            {
				if (items.Exception == null)
				{
					this.Items = new ObservableCollection<Item>(items.Result);
					base.OnPropertyChanged(nameof(this.Items));
				}
            });

            this.Items = new ObservableCollection<Item>();
        }

        public ObservableCollection<Item> Items
        {
            get;
            private set;
        }

		public string Title
		{
			get;
		}
    }
}