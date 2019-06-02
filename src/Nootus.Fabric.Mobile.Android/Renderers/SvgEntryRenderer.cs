using Android.Content;
using Android.Content.Res;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Support.V4.Content;
using Nootus.Fabric.Mobile.Controls;
using Nootus.Fabric.Mobile.Droid.Renderers;
using SkiaSharp.Views.Android;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(SvgEntry), typeof(SvgEntryRenderer))]
namespace Nootus.Fabric.Mobile.Droid.Renderers
{
    public class SvgEntryRenderer : EntryRenderer
    {
        SvgEntry element;
        public SvgEntryRenderer(Context context) : base(context)
        {

        }
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null || e.NewElement == null)
                return;

            element = (SvgEntry)this.Element;


            var editText = this.Control;
            if (!string.IsNullOrEmpty(element.Svg))
            {
                switch (element.SvgAlignment)
                {
                    case SvgAlignment.Left:
                        editText.SetCompoundDrawablesWithIntrinsicBounds(GetDrawable(element.Svg), null, null, null);
                        break;
                    case SvgAlignment.Right:
                        editText.SetCompoundDrawablesWithIntrinsicBounds(null, null, GetDrawable(element.Svg), null);
                        break;
                }
            }
            editText.CompoundDrawablePadding = 25;
            Control.Background.SetColorFilter(element.LineColor.ToAndroid(), PorterDuff.Mode.SrcAtop);
        }

        private BitmapDrawable GetDrawable(string svgEntryImage)
        {
            var svg = SvgIcon.GetSvgBitmap(svgEntryImage, element.SvgWidthRequest, element.SvgHeightRequest, element.SvgColor);
            return new BitmapDrawable(Resources, svg.ToBitmap());
        }

    }
}