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
    public partial class RoundImage : Frame
    {
        public RoundImage()
        {
            InitializeComponent();
        }

        public static readonly BindableProperty SvgSourceProperty = BindableProperty.Create(nameof(Source), typeof(ImageSource), typeof(RoundImage), default(ImageSource), BindingMode.OneWay,
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
                return (ImageSource)GetValue(SvgSourceProperty);
            }

            set
            {
                SetValue(SvgSourceProperty, value);
            }
        }

    }
}