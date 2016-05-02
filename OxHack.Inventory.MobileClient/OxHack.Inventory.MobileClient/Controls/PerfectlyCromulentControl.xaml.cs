using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace OxHack.Inventory.MobileClient.Controls
{
    public partial class PerfectlyCromulentControl : StackLayout
    {
        public PerfectlyCromulentControl()
        {
            InitializeComponent();

            this.a.Opacity = 0;
            this.a.TranslationX -= 100;
            this.app.Opacity = 0;
            this.app.TranslationX += 100;
            this.perfectlyCromulent.Opacity = 0;
        }

        internal async Task BeginAnimationAsync()
        {
            uint shortDuration = 1500;
			uint longDuration = 1600;

			this.a.FadeTo(1, longDuration, Easing.CubicIn);
            this.a.TranslateTo(this.a.TranslationX + 100, this.a.TranslationY, longDuration, Easing.CubicOut);

            this.app.FadeTo(1, longDuration, Easing.CubicIn);
            this.app.TranslateTo(this.app.TranslationX - 100, this.app.TranslationY, longDuration, Easing.CubicOut);

            await this.perfectlyCromulent.FadeTo(1, shortDuration, Easing.CubicIn);
        }
    }
}
