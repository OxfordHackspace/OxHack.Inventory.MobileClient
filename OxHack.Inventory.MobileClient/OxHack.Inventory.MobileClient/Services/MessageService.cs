using Acr.UserDialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OxHack.Inventory.MobileClient.Services
{
	public class MessageService
	{
		public void ShowToast(string message)
		{
			try
			{
				UserDialogs.Instance.Toast(message);
			}
			catch
			{
				// DO:  Gobble up error.  Toasts should not be able to take down the application :)
				// TODO: log error
			}
		}

		public void ShowError(string message)
		{
			try
			{
				UserDialogs.Instance.ShowError(message);
			}
			catch
			{
				// DO:  Gobble up error.  Error messages should not be able to take down the application :)
				// TODO: log error
			}
		}
	}
}
