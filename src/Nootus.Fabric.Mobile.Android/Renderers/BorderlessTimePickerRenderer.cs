using Android.Content;
using Nootus.Fabric.Mobile.Controls;
using Nootus.Fabric.Mobile.Droid.Renderers;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(BorderlessTimePicker), typeof(BorderlessTimePickerRenderer))]
namespace Nootus.Fabric.Mobile.Droid.Renderers
{
    public class BorderlessTimePickerRenderer : TimePickerRenderer
    {
        private Color textColor, placeholderColor;
        public BorderlessTimePickerRenderer(Context context) : base(context) { }

        protected override void OnElementChanged(ElementChangedEventArgs<TimePicker> e)
        {
            base.OnElementChanged(e);
            if (e.OldElement == null)
            {
                Control.Background = null;
                var layoutParams = new MarginLayoutParams(Control.LayoutParameters);
                layoutParams.SetMargins(0, 0, 0, 0);
                LayoutParameters = layoutParams;
                Control.LayoutParameters = layoutParams;
                Control.SetPadding(0, 0, 0, 0);
                SetPadding(0, 0, 0, 0);

                BorderlessTimePicker element = Element as BorderlessTimePicker;

                if (element.Time == TimeSpan.Zero && !String.IsNullOrEmpty(element.Placeholder))
                {
                    Control.Text = element.Placeholder;
                    textColor = element.TextColor;
                    placeholderColor = element.PlaceholderColor == default(Color) ? textColor : element.PlaceholderColor;

                    element.TextColor = placeholderColor;
                    Control.TextChanged += Control_TextChanged;
                }
            }
        }

        private void Control_TextChanged(object sender, Android.Text.TextChangedEventArgs e)
        {
            Element.TextColor = textColor;
        }
    }
}
