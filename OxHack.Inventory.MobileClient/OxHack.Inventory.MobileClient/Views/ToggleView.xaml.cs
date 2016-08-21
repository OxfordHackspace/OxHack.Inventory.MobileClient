using Xamarin.Forms;

namespace OxHack.Inventory.MobileClient.Views
{
    public partial class ToggleView : StackLayout
    {
		public ToggleView()
		{
			this.InitializeComponent();
		}

		public string ToggledTrueText
		{
			get
			{
				return (string)this.GetValue(ToggleView.ToggledTrueTextProperty);
			}
			set
			{
				this.SetValue(ToggleView.ToggledTrueTextProperty, value);
			}
		}

		public static readonly BindableProperty ToggledTrueTextProperty =
			BindableProperty.Create(nameof(ToggleView.ToggledTrueText), typeof(string), typeof(ToggleView), true.ToString());

		public string ToggledFalseText
		{
			get
			{
				return (string)this.GetValue(ToggleView.ToggledFalseTextProperty);
			}
			set
			{
				this.SetValue(ToggleView.ToggledFalseTextProperty, value);
			}
		}

		public static readonly BindableProperty ToggledFalseTextProperty =
			BindableProperty.Create(nameof(ToggleView.ToggledFalseText), typeof(string), typeof(ToggleView), false.ToString());
	}
}
