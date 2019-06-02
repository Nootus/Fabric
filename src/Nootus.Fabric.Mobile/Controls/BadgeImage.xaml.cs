using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Nootus.Fabric.Mobile.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BadgeImage : ContentView
    {
        public BadgeImage()
        {
            InitializeComponent();
        }

        public static readonly BindableProperty ImageSourceProperty = BindableProperty.Create(nameof(ImageSource), typeof(ImageSource), typeof(BadgeImage), default(ImageSource), BindingMode.OneWay,
            propertyChanged:
            (BindableObject bindable, object oldValue, object newValue) =>
            {
                ((BadgeImage)bindable).img.Source = (ImageSource)newValue;
            }
        );
        public ImageSource ImageSource
        {
            get
            {
                return (ImageSource)GetValue(ImageSourceProperty);
            }

            set
            {
                SetValue(ImageSourceProperty, value);
            }
        }

        public static readonly BindableProperty ImageDiameterProperty = BindableProperty.Create(nameof(ImageDiameter), typeof(int), typeof(BadgeImage), default(int), BindingMode.OneWay,
            propertyChanged:
                (BindableObject bindable, object oldValue, object newValue) =>
                {
                    ((BadgeImage)bindable).img.Diameter = (int)newValue;
                }
        );

        public int ImageDiameter
        {
            get
            {
                return (int)GetValue(ImageDiameterProperty);
            }

            set
            {
                SetValue(ImageDiameterProperty, value);
            }
        }

        public static readonly BindableProperty SvgSourceProperty = BindableProperty.Create(nameof(SvgSource), typeof(ImageSource), typeof(BadgeImage), default(ImageSource), BindingMode.OneWay,
            propertyChanged:
            (BindableObject bindable, object oldValue, object newValue) =>
            {
                ((BadgeImage)bindable).svg.Source = (ImageSource)newValue;
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

        public static readonly BindableProperty SvgDiameterProperty = BindableProperty.Create(nameof(SvgDiameter), typeof(int), typeof(BadgeImage), default(int), BindingMode.OneWay,
            propertyChanged:
                (BindableObject bindable, object oldValue, object newValue) =>
                {
                    ((BadgeImage)bindable).svg.HeightRequest = (int)newValue;
                    ((BadgeImage)bindable).svg.WidthRequest = (int)newValue;
                }
        );

        public int SvgDiameter
        {
            get
            {
                return (int)GetValue(SvgDiameterProperty);
            }

            set
            {
                SetValue(SvgDiameterProperty, value);
            }
        }

        public static readonly BindableProperty ShowBadgeProperty = BindableProperty.Create(nameof(ShowBadge), typeof(bool), typeof(BadgeImage), false, BindingMode.OneWay,
            propertyChanged:
                (BindableObject bindable, object oldValue, object newValue) =>
                {
                    ((BadgeImage)bindable).badgeFrame.IsVisible = (bool)newValue;
                }
        );

        public bool ShowBadge
        {
            get
            {
                return (bool)GetValue(ShowBadgeProperty);
            }

            set
            {
                SetValue(ShowBadgeProperty, value);
            }
        }
    }
}