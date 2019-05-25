using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Nootus.Fabric.Mobile.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BorderlessSvgDatePicker : BorderlessSvgStackLayout
    {
        public BorderlessSvgDatePicker()
        {
            InitializeComponent();
        }

        public override void SetSvgHexColor(string color)
        {
            svgTrans.HexColor = color;
        }

        public static readonly BindableProperty DateProperty = BindableProperty.Create(nameof(Date), typeof(DateTime), typeof(BorderlessSvgDatePicker), default(DateTime), BindingMode.TwoWay);
        public DateTime Date
        {
            get
            {
                return (DateTime)GetValue(DateProperty);
            }

            set
            {
                SetValue(DateProperty, value);
            }
        }

        public static readonly BindableProperty MinimumDateProperty = BindableProperty.Create(nameof(MinimumDate), typeof(DateTime), typeof(BorderlessSvgDatePicker), default(DateTime), BindingMode.OneWay,
            propertyChanged:
                (BindableObject bindable, object oldValue, object newValue) =>
                {
                    ((BorderlessSvgDatePicker)bindable).datePicker.MinimumDate = (DateTime)newValue;
                });

        public DateTime MinimumDate
        {
            get
            {
                return (DateTime)GetValue(MinimumDateProperty);
            }

            set
            {
                SetValue(MinimumDateProperty, value);
            }
        }

        public static readonly BindableProperty MaximumDateProperty = BindableProperty.Create(nameof(MaximumDate), typeof(DateTime), typeof(BorderlessSvgDatePicker), default(DateTime), BindingMode.OneWay,
            propertyChanged:
                (BindableObject bindable, object oldValue, object newValue) =>
                {
                    ((BorderlessSvgDatePicker)bindable).datePicker.MaximumDate = (DateTime)newValue;
                });
        public DateTime MaximumDate
        {
            get
            {
                return (DateTime)GetValue(MaximumDateProperty);
            }

            set
            {
                SetValue(MaximumDateProperty, value);
            }
        }
    }
}