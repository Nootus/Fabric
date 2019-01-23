using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Nootus.Fabric.Mobile.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RoundSvg : Frame
    {
        public static readonly BindableProperty SvgSourceProperty = BindableProperty.Create(nameof(SvgSource), typeof(ImageSource), typeof(RoundSvg), default(ImageSource), BindingMode.OneWay);
        public static readonly BindableProperty SvgWidthRequestProperty = BindableProperty.Create(nameof(SvgWidthRequest), typeof(double), typeof(RoundSvg), default(double), BindingMode.OneWay);
        public static readonly BindableProperty SvgHeightRequestProperty = BindableProperty.Create(nameof(SvgHeightRequest), typeof(double), typeof(RoundSvg), default(double), BindingMode.OneWay);
        public static readonly BindableProperty SvgHexColorProperty = BindableProperty.Create(nameof(SvgHexColor), typeof(string), typeof(RoundSvg), default(string), BindingMode.OneWay);

        public RoundSvg()
        {
            InitializeComponent();
            this.BindingContext = this;
        }

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

        protected override void OnPropertyChanged(string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == SvgSourceProperty.PropertyName)
            {
//                svg.Source = SvgSource;
            }
            else if (propertyName == SvgWidthRequestProperty.PropertyName)
            {
                svg.WidthRequest = SvgWidthRequest;
            }
            else if (propertyName == SvgHeightRequestProperty.PropertyName)
            {
                svg.HeightRequest = SvgHeightRequest;
            }
            else if (propertyName == SvgHexColorProperty.PropertyName)
            {
                svgTrans.HexColor = SvgHexColor;
            }
        }
    }
}