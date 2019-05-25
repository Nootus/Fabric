using System;
using System.Globalization;
using Xamarin.Forms;

namespace Nootus.Fabric.Mobile.Converters
{
    public class ZeroToNullConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int && System.Convert.ToInt32(value) == 0)
            {
                return null;
            }

            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || String.IsNullOrWhiteSpace(value.ToString()))
                return 0;
            return int.Parse(value.ToString());
        }
    }
}
