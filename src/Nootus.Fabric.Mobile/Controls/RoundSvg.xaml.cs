using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Nootus.Fabric.Mobile.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RoundSvg : Frame, ICloneable
    {
        public RoundSvg()
        {
            InitializeComponent();
        }

        public static readonly BindableProperty SvgSourceProperty = BindableProperty.Create(nameof(SvgSource), typeof(ImageSource), typeof(RoundSvg), default(ImageSource), BindingMode.OneWay,
            propertyChanged:
                (BindableObject bindable, object oldValue, object newValue) =>
                {
                    ((RoundSvg)bindable).svg.Source = (ImageSource)newValue;
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

        public static readonly BindableProperty SvgWidthRequestProperty = BindableProperty.Create(nameof(SvgWidthRequest), typeof(double), typeof(RoundSvg), default(double), BindingMode.OneWay,
            propertyChanged:
                (BindableObject bindable, object oldValue, object newValue) =>
                {
                    ((RoundSvg)bindable).svg.WidthRequest = (double) newValue;
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


        public static readonly BindableProperty SvgHeightRequestProperty = BindableProperty.Create(nameof(SvgHeightRequest), typeof(double), typeof(RoundSvg), default(double), BindingMode.OneWay,
            propertyChanged:
                (BindableObject bindable, object oldValue, object newValue) =>
                {
                    ((RoundSvg) bindable).svg.HeightRequest = (double) newValue;
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

        public static readonly BindableProperty SvgHexColorProperty = BindableProperty.Create(nameof(SvgHexColor), typeof(string), typeof(RoundSvg), default(string), BindingMode.OneWay,
            propertyChanged:
                (BindableObject bindable, object oldValue, object newValue) =>
                {
                    ((RoundSvg) bindable).svgTrans.HexColor = (string) newValue;
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

        public static readonly BindableProperty SvgRotationProperty = BindableProperty.Create(nameof(SvgRotation), typeof(double), typeof(RoundSvg), default(double), BindingMode.OneWay,
            propertyChanged:
                (BindableObject bindable, object oldValue, object newValue) =>
                {
                    ((RoundSvg)bindable).svg.Rotation = (double)newValue;
                }
        );
        public double SvgRotation
        {
            get
            {
                return (double)GetValue(SvgRotationProperty);
            }

            set
            {
                SetValue(SvgRotationProperty, value);
            }
        }

        public object CommandParameter { get; set; }

        public object Clone()
        {
            RoundSvg svg = new RoundSvg()
            {
                BackgroundColor = this.BackgroundColor,
                SvgWidthRequest = this.SvgWidthRequest,
                SvgHeightRequest = this.SvgHeightRequest,
                SvgHexColor = this.SvgHexColor,
                HorizontalOptions = this.HorizontalOptions,
                VerticalOptions = this.VerticalOptions
            };

            return svg;
        }
    }
}