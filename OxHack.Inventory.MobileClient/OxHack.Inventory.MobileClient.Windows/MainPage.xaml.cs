using System.Reflection;

namespace OxHack.Inventory.MobileClient.Windows
{
	public sealed partial class MainPage
	{
		public MainPage()
		{
			this.InitializeComponent();

			var app = new MobileClient.App(this.GetType().GetTypeInfo().Assembly);

			LoadApplication(app);
		}
	}
}
