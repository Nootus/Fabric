using Android.Content;
using Nootus.Fabric.Mobile.Controls;
using Nootus.Fabric.Mobile.Droid.Renderers;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using static Android.App.DatePickerDialog;

[assembly: ExportRenderer(typeof(BorderlessDatePicker), typeof(BorderlessDatePickerRenderer))]
namespace Nootus.Fabric.Mobile.Droid.Renderers
{
    public class BorderlessDatePickerRenderer : DatePickerRenderer
    {
        private Color textColor, placeholderColor;
        public BorderlessDatePickerRenderer(Context context) : base(context) { }

        protected override void OnElementChanged(ElementChangedEventArgs<DatePicker> e)
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

                BorderlessDatePicker element = Element as BorderlessDatePicker;

                if (element.Date == element.MinimumDate && !String.IsNullOrEmpty(element.Placeholder))
                {
                    Control.Text = element.Placeholder;
                    textColor = element.TextColor;
                    placeholderColor = element.PlaceholderColor == default(Color) ? textColor : element.PlaceholderColor;

                    element.TextColor = placeholderColor;
                    Control.Click += Control_Click;
                }
            }
        }

        private void Control_Click(object sender, EventArgs e)
        {
            var dialog = CreateDatePickerDialog(Element.Date.Year, Element.Date.Month - 1, Element.Date.Day);
            dialog.DatePicker.MinDate = (long)Element.MinimumDate.ToUniversalTime().Subtract(DateTime.MinValue.AddYears(1969)).TotalMilliseconds;
            dialog.DatePicker.MaxDate = (long)Element.MaximumDate.ToUniversalTime().Subtract(DateTime.MinValue.AddYears(1969)).TotalMilliseconds;

            dialog.DateSet += Dialog_DateSet;
            dialog.Show();
        }

        private void Dialog_DateSet(object sender, DateSetEventArgs e)
        {
            BorderlessDatePicker element = Element as BorderlessDatePicker;
            element.Date = e.Date;
            if(Control.Text == element.Placeholder)
            {
                Control.Text = element.Date.ToString(Element.Format);
                element.TextColor = textColor;
            }
        }
    }
}
