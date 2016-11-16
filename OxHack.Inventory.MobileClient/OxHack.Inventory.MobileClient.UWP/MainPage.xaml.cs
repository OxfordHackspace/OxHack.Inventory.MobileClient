using System.Reflection;

namespace OxHack.Inventory.MobileClient.UWP
{
	public sealed partial class MainPage
	{
		public MainPage()
		{
			this.InitializeComponent();

			LoadApplication(new OxHack.Inventory.MobileClient.App(this.GetType().GetTypeInfo().Assembly));
		}
	}
}
