using Android.Content;
using Android.Graphics.Drawables;
using Nootus.Fabric.Mobile.Controls;
using Nootus.Fabric.Mobile.Droid.Renderers;
using SkiaSharp;
using SkiaSharp.Views.Android;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android.AppCompat;

// [assembly: ExportRenderer(typeof(TabbedPage), typeof(SvgTabbedPageRenderer))]
namespace Nootus.Fabric.Mobile.Droid.Renderers
{
    public class SvgTabbedPageRenderer : TabbedPageRenderer
    {
        private Context context;

        public SvgTabbedPageRenderer(Context context) : base(context)
        {
            this.context = context;    
        }

        protected override Android.Graphics.Drawables.Drawable GetIconDrawable(FileImageSource icon)
        {
            if (icon.File.StartsWith("svg:"))
            {
                string resourceid = icon.File.Replace("svg:", "");
                SKBitmap skBitmap = SvgIcon.GetSvgBitmap(resourceid, 100, 100, new Xamarin.Forms.Color());
                return new BitmapDrawable(context.Resources, skBitmap.ToBitmap());
            }
            else
                return base.GetIconDrawable(icon);
        }
    }
}