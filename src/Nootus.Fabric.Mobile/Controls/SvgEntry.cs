using Xamarin.Forms;

namespace Nootus.Fabric.Mobile.Controls
{
    public class SvgEntry: Entry
    {
        public SvgEntry()
        {
            this.HeightRequest = 50;
        }

        public static readonly BindableProperty LineColorProperty =
            BindableProperty.Create(nameof(LineColor), typeof(Color), typeof(SvgEntry), Color.Default);
        public Color LineColor
        {
            get { return (Color)GetValue(LineColorProperty); }
            set { SetValue(LineColorProperty, value); }
        }

        public static readonly BindableProperty SvgProperty =
            BindableProperty.Create(nameof(Svg), typeof(string), typeof(SvgEntry), string.Empty);
        public string Svg
        {
            get { return (string)GetValue(SvgProperty); }
            set { SetValue(SvgProperty, value); }
        }

        public static readonly BindableProperty SvgHeightRequestProperty =
            BindableProperty.Create(nameof(SvgHeightRequest), typeof(int), typeof(SvgEntry), 56);
        public int SvgHeightRequest
        {
            get { return (int)GetValue(SvgHeightRequestProperty); }
            set { SetValue(SvgHeightRequestProperty, value); }
        }

        public static readonly BindableProperty SvgWidthRequestProperty =
            BindableProperty.Create(nameof(SvgWidthRequest), typeof(int), typeof(SvgEntry), 56);
        public int SvgWidthRequest
        {
            get { return (int)GetValue(SvgWidthRequestProperty); }
            set { SetValue(SvgWidthRequestProperty, value); }
        }

        public static readonly BindableProperty SvgAlignmentProperty =
            BindableProperty.Create(nameof(SvgAlignment), typeof(SvgAlignment), typeof(SvgEntry), SvgAlignment.Right);
        public SvgAlignment SvgAlignment
        {
            get { return (SvgAlignment)GetValue(SvgAlignmentProperty); }
            set { SetValue(SvgAlignmentProperty, value); }
        }

        public static readonly BindableProperty SvgColorProperty =
            BindableProperty.Create(nameof(SvgColor), typeof(Color), typeof(SvgEntry), Color.Default);
        public Color SvgColor
        {
            get { return (Color)GetValue(SvgColorProperty); }
            set { SetValue(SvgColorProperty, value); }
        }

    }

    public enum SvgAlignment
    {
        Left,
        Right
    }
}
