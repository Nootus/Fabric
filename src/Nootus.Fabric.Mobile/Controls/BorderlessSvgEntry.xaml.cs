using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Nootus.Fabric.Mobile.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BorderlessSvgEntry : BorderlessSvgStackLayout
    {
        public BorderlessSvgEntry()
        {
            InitializeComponent();
        }

        public override void SetSvgHexColor(string color)
        {
            svgTrans.HexColor = color;
        }

        public static readonly BindableProperty TextProperty = BindableProperty.Create(nameof(Text), typeof(string), typeof(BorderlessSvgEntry), default(string), BindingMode.TwoWay);
        public string Text
        {
            get
            {
                return (string)GetValue(TextProperty);
            }

            set
            {
                SetValue(TextProperty, value);
            }
        }

        public static readonly BindableProperty KeyboardProperty = BindableProperty.Create(nameof(Keyboard), typeof(Keyboard), typeof(BorderlessSvgEntry), default(Keyboard), BindingMode.OneWay);
        public Keyboard Keyboard
        {
            get
            {
                return (Keyboard)GetValue(KeyboardProperty);
            }

            set
            {
                SetValue(KeyboardProperty, value);
            }
        }
    }
}