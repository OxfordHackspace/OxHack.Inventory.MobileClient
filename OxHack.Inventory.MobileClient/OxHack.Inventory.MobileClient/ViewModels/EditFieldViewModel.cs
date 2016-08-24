using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OxHack.Inventory.MobileClient.ViewModels
{
	public class EditFieldViewModel<T> : EditFieldViewModelBase
	{
		private T value;
		private T editedValue;

		public EditFieldViewModel(Action onSave = null)
		{
			this.BeginEditCommand =
				new DelegateCommand(
					() =>
					{
						this.EditedValue = this.Value;
						base.OnPropertyChanged(nameof(this.EditedValue));
						this.IsInEditMode = true;
					},
					() => this.IsNotInEditMode);

			this.DiscardChangesCommand =
				new DelegateCommand(
					() =>
					{
						this.EditedValue = this.Value;
						base.OnPropertyChanged(nameof(this.EditedValue));
						this.IsInEditMode = false;
					},
					() => this.IsInEditMode);

			this.SaveChangesCommand =
				new DelegateCommand(
					() =>
					{
						this.Value = this.EditedValue;
						onSave?.Invoke();
						this.IsInEditMode = false;
					},
					() => this.IsInEditMode);
		}

		public T Value
		{
			get
			{
				return this.value;
			}
			set
			{
				base.SetProperty(ref this.value, value);
			}
		}

		public T EditedValue
		{
			get
			{
				return this.editedValue;
			}
			set
			{
				base.SetProperty(ref this.editedValue, value);
			}
		}
	}

	public abstract class EditFieldViewModelBase : BindableBase
	{
		private bool isInEditMode;
		private bool isEditEnabled = true;

		public bool IsInEditMode
		{
			get
			{
				return this.isInEditMode;
			}
			set
			{
				base.SetProperty(ref this.isInEditMode, value);
				base.OnPropertyChanged(nameof(this.IsNotInEditMode));
				this.BeginEditCommand.RaiseCanExecuteChanged();
				this.DiscardChangesCommand.RaiseCanExecuteChanged();
				this.SaveChangesCommand.RaiseCanExecuteChanged();
			}
		}

		public bool IsNotInEditMode
			=> !this.IsInEditMode;

		public bool IsEditEnabled
		{
			get
			{
				return this.isEditEnabled;
			}
			set
			{
				base.SetProperty(ref this.isEditEnabled, value);
			}
		}

		public DelegateCommand BeginEditCommand
		{
			get;
			set;
		}

		public DelegateCommand SaveChangesCommand
		{
			get;
			set;
		}

		public DelegateCommand DiscardChangesCommand
		{
			get;
			set;
		}
	}
}