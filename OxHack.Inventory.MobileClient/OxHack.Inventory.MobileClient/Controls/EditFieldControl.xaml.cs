using Xamarin.Forms;

namespace OxHack.Inventory.MobileClient.Controls
{
    public partial class EditFieldControl : StackLayout
    {
		public EditFieldControl()
		{
			this.InitializeComponent();
			this.editButton.Clicked += (s, e) =>
			{
				entry.Focus();
			};
		}

		public Keyboard Keyboard
		{
			get
			{
				return (Keyboard)this.GetValue(EditFieldControl.KeyboardProperty) ?? Keyboard.Default;
			}
			set
			{
				this.SetValue(EditFieldControl.KeyboardProperty, value);
			}
		}

		public static readonly BindableProperty KeyboardProperty =
			BindableProperty.Create(nameof(EditFieldControl.Keyboard), typeof(Keyboard), typeof(EditFieldControl), Keyboard.Default);
	}
}
