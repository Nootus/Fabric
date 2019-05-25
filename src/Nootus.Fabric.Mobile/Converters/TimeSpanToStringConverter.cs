using System;
using System.Globalization;
using Xamarin.Forms;

namespace Nootus.Fabric.Mobile.Converters
{
    public class TimeSpanToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            DateTime dt = DateTime.Today.Add((TimeSpan)value);
            return dt.ToString("h:mm tt");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
