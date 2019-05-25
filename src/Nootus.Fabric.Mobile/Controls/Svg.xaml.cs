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
    public partial class Svg : ContentView
    {
        public Svg()
        {
            InitializeComponent();
        }

        public static readonly BindableProperty SvgSourceProperty = BindableProperty.Create(nameof(SvgSource), typeof(ImageSource), typeof(Svg), default(ImageSource), BindingMode.OneWay,
            propertyChanged:
                (BindableObject bindable, object oldValue, object newValue) =>
                {
                    ((Svg)bindable).svg.Source = (ImageSource)newValue;
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

        public static readonly BindableProperty SvgWidthRequestProperty = BindableProperty.Create(nameof(SvgWidthRequest), typeof(double), typeof(Svg), default(double), BindingMode.OneWay,
            propertyChanged:
                (BindableObject bindable, object oldValue, object newValue) =>
                {
                    ((Svg)bindable).svg.WidthRequest = (double)newValue;
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


        public static readonly BindableProperty SvgHeightRequestProperty = BindableProperty.Create(nameof(SvgHeightRequest), typeof(double), typeof(Svg), default(double), BindingMode.OneWay,
            propertyChanged:
                (BindableObject bindable, object oldValue, object newValue) =>
                {
                    ((Svg)bindable).svg.HeightRequest = (double)newValue;
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

        public static readonly BindableProperty SvgHexColorProperty = BindableProperty.Create(nameof(SvgHexColor), typeof(string), typeof(Svg), default(string), BindingMode.OneWay,
            propertyChanged:
                (BindableObject bindable, object oldValue, object newValue) =>
                {
                    ((Svg)bindable).svgTrans.HexColor = (string)newValue;
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
    }
}