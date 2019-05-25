using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Nootus.Fabric.Mobile.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BorderlessSvgTimePicker : BorderlessSvgStackLayout
    {
        public BorderlessSvgTimePicker()
        {
            InitializeComponent();
        }

        public override void SetSvgHexColor(string color)
        {
            svgTrans.HexColor = color;
        }

        public static readonly BindableProperty TimeProperty = BindableProperty.Create(nameof(Time), typeof(TimeSpan), typeof(BorderlessSvgTimePicker), default(TimeSpan), BindingMode.TwoWay);
        public TimeSpan Time
        {
            get
            {
                return (TimeSpan)GetValue(TimeProperty);
            }

            set
            {
                SetValue(TimeProperty, value);
            }
        }
    }
}