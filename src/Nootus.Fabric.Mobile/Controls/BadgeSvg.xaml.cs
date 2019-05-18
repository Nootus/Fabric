using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Nootus.Fabric.Mobile.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BadgeSvg : ContentView
    {
        public BadgeSvg()
        {
            InitializeComponent();
        }

        public static readonly BindableProperty SvgSourceProperty = BindableProperty.Create(nameof(SvgSource), typeof(ImageSource), typeof(BadgeSvg), default(ImageSource), BindingMode.OneWay,
            propertyChanged:
                (BindableObject bindable, object oldValue, object newValue) =>
                {
                    ((BadgeSvg)bindable).svg.Source = (ImageSource)newValue;
                }
            );

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

        public static readonly BindableProperty SvgWidthRequestProperty = BindableProperty.Create(nameof(SvgWidthRequest), typeof(double), typeof(BadgeSvg), default(double), BindingMode.OneWay,
            propertyChanged:
                (BindableObject bindable, object oldValue, object newValue) =>
                {
                    ((BadgeSvg)bindable).svg.WidthRequest = (double)newValue;
                }
            );

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

        public static readonly BindableProperty SvgHeightRequestProperty = BindableProperty.Create(nameof(SvgHeightRequest), typeof(double), typeof(BadgeSvg), default(double), BindingMode.OneWay,
            propertyChanged:
                (BindableObject bindable, object oldValue, object newValue) =>
                {
                    ((BadgeSvg)bindable).svg.HeightRequest = (double)newValue;
                }
            );

        public double SvgHeightRequest
        {
            get
            {
                return (double)GetValue(SvgHeightRequestProperty);
            }

            set
            {
                SetValue(SvgWidthRequestProperty, value);
            }
        }

        public static readonly BindableProperty SvgHexColorProperty = BindableProperty.Create(nameof(SvgHexColor), typeof(string), typeof(BadgeSvg), default(string), BindingMode.OneWay,
            propertyChanged:
                (BindableObject bindable, object oldValue, object newValue) =>
                {
                    ((BadgeSvg)bindable).svgTrans.HexColor = (string)newValue;
                }
            );

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

        public static readonly BindableProperty BadgeBackgroundColorProperty = BindableProperty.Create(nameof(BadgeBackgroundColor), typeof(Color), typeof(BadgeSvg), default(Color), BindingMode.OneWay,
            propertyChanged:
                (BindableObject bindable, object oldValue, object newValue) =>
                {
                    ((BadgeSvg)bindable).badgeFrame.BackgroundColor = (Color)newValue;
                }
            );

        public Color BadgeBackgroundColor
        {
            get
            {
                return (Color)GetValue(BadgeBackgroundColorProperty);
            }

            set
            {
                SetValue(BadgeBackgroundColorProperty, value);
            }
        }

        public static readonly BindableProperty BadgeTextProperty = BindableProperty.Create(nameof(BadgeText), typeof(string), typeof(BadgeSvg), default(string), BindingMode.OneWay,
            propertyChanged:
                (BindableObject bindable, object oldValue, object newValue) =>
                {
                    ((BadgeSvg)bindable).badgeText.Text  = (string)newValue;
                }
            );

        public string BadgeText
        {
            get
            {
                return (string)GetValue(BadgeTextProperty);
            }

            set
            {
                SetValue(BadgeTextProperty, value);
            }
        }

        public static readonly BindableProperty BadgeWidthRequestProperty = BindableProperty.Create(nameof(BadgeWidthRequest), typeof(double), typeof(BadgeSvg), default(double), BindingMode.OneWay,
            propertyChanged:
                (BindableObject bindable, object oldValue, object newValue) =>
                {
                    double widthRequest = (double)newValue;
                    Frame badgeFrame = ((BadgeSvg)bindable).badgeFrame;

                    badgeFrame.WidthRequest = widthRequest;
                    badgeFrame.HeightRequest = widthRequest;
                    badgeFrame.CornerRadius = (float) widthRequest;
                }
            );

        public double BadgeWidthRequest
        {
            get
            {
                return (double)GetValue(BadgeWidthRequestProperty);
            }

            set
            {
                SetValue(BadgeWidthRequestProperty, value);
            }
        }

        public static readonly BindableProperty BadgeTextColorProperty = BindableProperty.Create(nameof(BadgeTextColor), typeof(Color), typeof(BadgeSvg), default(Color), BindingMode.OneWay,
            propertyChanged:
                (BindableObject bindable, object oldValue, object newValue) =>
                {
                    ((BadgeSvg)bindable).badgeText.TextColor = (Color)newValue;
                }
            );

        public Color BadgeTextColor
        {
            get
            {
                return (Color)GetValue(BadgeTextColorProperty);
            }

            set
            {
                SetValue(BadgeTextColorProperty, value);
            }
        }

        public static readonly BindableProperty BadgeFontSizeProperty = BindableProperty.Create(nameof(BadgeFontSize), typeof(double), typeof(BadgeSvg), default(double), BindingMode.OneWay,
            propertyChanged:
                (BindableObject bindable, object oldValue, object newValue) =>
                {
                    ((BadgeSvg)bindable).badgeText.FontSize = (double)newValue;
                }
            );

        public double BadgeFontSize
        {
            get
            {
                return (double)GetValue(BadgeFontSizeProperty);
            }

            set
            {
                SetValue(BadgeFontSizeProperty, value);
            }
        }
    }
}