using Xamarin.Forms;

namespace OxHack.Inventory.MobileClient.Views
{
    public partial class EditFieldView : StackLayout
    {
		public EditFieldView()
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
				return (Keyboard)this.GetValue(EditFieldView.KeyboardProperty) ?? Keyboard.Default;
			}
			set
			{
				this.SetValue(EditFieldView.KeyboardProperty, value);
			}
		}

		public static readonly BindableProperty KeyboardProperty =
			BindableProperty.Create(nameof(EditFieldView.Keyboard), typeof(Keyboard), typeof(EditFieldView), Keyboard.Default);
	}
}
