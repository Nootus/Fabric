using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Nootus.Fabric.Mobile.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RoundImage : Frame
    {
        public RoundImage()
        {
            InitializeComponent();
        }

        public static readonly BindableProperty SourceProperty = BindableProperty.Create(nameof(Source), typeof(ImageSource), typeof(RoundImage), default(ImageSource), BindingMode.OneWay,
            propertyChanged:
                (BindableObject bindable, object oldValue, object newValue) =>
                {
                    ((RoundImage)bindable).img.Source = (ImageSource)newValue;
                }
        );

        public ImageSource Source
        {
            get
            {
                return (ImageSource)GetValue(SourceProperty);
            }

            set
            {
                SetValue(SourceProperty, value);
            }
        }

        public static readonly BindableProperty DiameterProperty = BindableProperty.Create(nameof(Diameter), typeof(int), typeof(RoundImage), default(int), BindingMode.OneWay,
            propertyChanged:
                (BindableObject bindable, object oldValue, object newValue) =>
                {
                    int diameter = (int)newValue;
                    int heightWidth = diameter * 2;

                    RoundImage control = (RoundImage)bindable;
                    control.CornerRadius = diameter;
                    control.HeightRequest = heightWidth;
                    control.WidthRequest = heightWidth;
                    control.img.HeightRequest = heightWidth;
                    control.img.WidthRequest = heightWidth;
                }
        );

        public int Diameter
        {
            get
            {
                return (int)GetValue(DiameterProperty);
            }

            set
            {
                SetValue(DiameterProperty, value);
            }
        }
    }
}