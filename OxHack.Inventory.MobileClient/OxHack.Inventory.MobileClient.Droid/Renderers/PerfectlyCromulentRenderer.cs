using Android.Graphics;
using Android.Widget;
using OxHack.Inventory.MobileClient.Controls;
using OxHack.Inventory.MobileClient.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(PCLabel), typeof(PerfectlyCromulentRenderer))]
[assembly: ExportRenderer(typeof(PCItalicsLabel), typeof(PerfectlyCromulentRenderer))]
namespace OxHack.Inventory.MobileClient.Droid.Renderers
{
    public class PerfectlyCromulentRenderer : LabelRenderer
    {
        public PerfectlyCromulentRenderer()
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Label> e)
        {
            base.OnElementChanged(e);
            var type = e.NewElement.GetType();

            Typeface font;
            if (type == typeof(PCItalicsLabel))
            {
                font = Typeface.CreateFromAsset(Forms.Context.Assets, "IMFeENit28P.ttf");
            }
            else
            {
                font = Typeface.CreateFromAsset(Forms.Context.Assets, "IMFeENrm28P.ttf");
            }
            this.Control.Typeface = font;
        }
    }
}