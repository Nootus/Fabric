using Xamarin.Forms;

namespace Nootus.Fabric.Mobile.Controls
{
    public abstract class BorderlessSvgStackLayout: StackLayout
    {
        public static readonly BindableProperty PlaceholderProperty = BindableProperty.Create(nameof(Placeholder), typeof(string), typeof(BorderlessSvgEntry), default(string), BindingMode.OneWay);
        public string Placeholder
        {
            get
            {
                return (string)GetValue(PlaceholderProperty);
            }

            set
            {
                SetValue(PlaceholderProperty, value);
            }
        }

        public static readonly BindableProperty PlaceholderColorProperty = BindableProperty.Create(nameof(PlaceholderColor), typeof(Color), typeof(BorderlessSvgEntry), default(Color), BindingMode.OneWay);
        public Color PlaceholderColor
        {
            get
            {
                return (Color)GetValue(PlaceholderColorProperty);
            }

            set
            {
                SetValue(PlaceholderColorProperty, value);
            }
        }

        public static readonly BindableProperty TextColorProperty = BindableProperty.Create(nameof(TextColor), typeof(Color), typeof(BorderlessSvgEntry), default(Color), BindingMode.OneWay);
        public Color TextColor
        {
            get
            {
                return (Color)GetValue(TextColorProperty);
            }

            set
            {
                SetValue(TextColorProperty, value);
            }
        }

        public static readonly BindableProperty SvgSourceProperty = BindableProperty.Create(nameof(SvgSource), typeof(ImageSource), typeof(BorderlessSvgEntry), default(ImageSource), BindingMode.OneWay);
        public ImageSource SvgSource
        {
            get
            {
                return (ImageSource)GetValue(SvgSourceProperty);
            }

            set
            {
                SetValue(SvgSourceProperty, value);
            }
        }

        public static readonly BindableProperty SvgHeightRequestProperty = BindableProperty.Create(nameof(SvgHeightRequest), typeof(double), typeof(BorderlessSvgEntry), default(double), BindingMode.OneWay);
        public double SvgHeightRequest
        {
            get
            {
                return (double)GetValue(SvgHeightRequestProperty);
            }

            set
            {
                SetValue(SvgHeightRequestProperty, value);
            }
        }

        public static readonly BindableProperty SvgWidthRequestProperty = BindableProperty.Create(nameof(SvgWidthRequest), typeof(double), typeof(BorderlessSvgEntry), default(double), BindingMode.OneWay);
        public double SvgWidthRequest
        {
            get
            {
                return (double)GetValue(SvgWidthRequestProperty);
            }

            set
            {
                SetValue(SvgWidthRequestProperty, value);
            }
        }

        public static readonly BindableProperty SvgHexColorProperty = BindableProperty.Create(nameof(SvgHexColor), typeof(string), typeof(BorderlessSvgEntry), default(string), BindingMode.OneWay,
            propertyChanged:
                (BindableObject bindable, object oldValue, object newValue) =>
                {
                    ((BorderlessSvgStackLayout)bindable).SetSvgHexColor((string)newValue);
                });
        public string SvgHexColor
        {
            get
            {
                return (string)GetValue(SvgHexColorProperty);
            }

            set
            {
                SetValue(SvgHexColorProperty, value);
            }
        }
        public abstract void SetSvgHexColor(string color);

    }
}
