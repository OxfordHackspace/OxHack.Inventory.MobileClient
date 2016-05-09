using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace OxHack.Inventory.MobileClient.Helpers
{
    public class ForPlatform<T> : OnPlatform<T>
    {
        public T WindowsRT
        {
            get;
            set;
        }

        public static implicit operator T(ForPlatform<T> @this)
        {
            if (Device.OS == TargetPlatform.Windows)
            {
                return @this.WindowsRT;
            }

            return (OnPlatform<T>)@this;
        }
    }
}
