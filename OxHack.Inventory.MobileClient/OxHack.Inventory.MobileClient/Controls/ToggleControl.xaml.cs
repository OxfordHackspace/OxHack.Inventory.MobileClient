using Xamarin.Forms;

namespace OxHack.Inventory.MobileClient.Controls
{
    public partial class ToggleControl : StackLayout
    {
		public ToggleControl()
		{
			this.InitializeComponent();
		}

		public string ToggledTrueText
		{
			get
			{
				return (string)this.GetValue(ToggleControl.ToggledTrueTextProperty);
			}
			set
			{
				this.SetValue(ToggleControl.ToggledTrueTextProperty, value);
			}
		}

		public static readonly BindableProperty ToggledTrueTextProperty =
			BindableProperty.Create(nameof(ToggleControl.ToggledTrueText), typeof(string), typeof(ToggleControl), true.ToString());

		public string ToggledFalseText
		{
			get
			{
				return (string)this.GetValue(ToggleControl.ToggledFalseTextProperty);
			}
			set
			{
				this.SetValue(ToggleControl.ToggledFalseTextProperty, value);
			}
		}

		public static readonly BindableProperty ToggledFalseTextProperty =
			BindableProperty.Create(nameof(ToggleControl.ToggledFalseText), typeof(string), typeof(ToggleControl), false.ToString());
	}
}
