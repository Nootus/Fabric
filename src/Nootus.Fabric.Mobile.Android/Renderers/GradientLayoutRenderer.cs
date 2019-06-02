using Android.Content;
using Android.Graphics.Drawables;
using Android.Support.V4.View;
using Nootus.Fabric.Mobile.Controls;
using Nootus.Fabric.Mobile.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(GradientLayout), typeof(GradientLayoutRenderer))]
namespace Nootus.Fabric.Mobile.Droid.Renderers
{
    public class GradientLayoutRenderer : VisualElementRenderer<StackLayout>
    {
        public GradientLayoutRenderer(Context ctx) : base(ctx)
        { }

        protected override void DispatchDraw(global::Android.Graphics.Canvas canvas)
        {
            GradientLayout layout = (GradientLayout)this.Element;

            Color[] Colors = layout.Colors;
            GradientColorStackMode Mode = layout.Mode;

            int[] colors = new int[Colors.Length];

            for (int i = 0, l = Colors.Length; i < l; i++)
            {
                colors[i] = Colors[i].ToAndroid().ToArgb();
            }

            // temporary workaround for Android 9 PIE
            if (Android.OS.Build.VERSION.SdkInt > Android.OS.BuildVersionCodes.O)
            {
                GradientDrawable.Orientation orientation;

                switch (Mode)
                {
                    default:
                    case GradientColorStackMode.ToRight:
                        orientation = GradientDrawable.Orientation.LeftRight;
                        break;
                    case GradientColorStackMode.ToTop:
                        orientation = GradientDrawable.Orientation.BottomTop;
                        break;
                    case GradientColorStackMode.ToBottom:
                        orientation = GradientDrawable.Orientation.TopBottom;
                        break;
                    case GradientColorStackMode.ToTopLeft:
                        orientation = GradientDrawable.Orientation.BrTl;
                        break;
                    case GradientColorStackMode.ToTopRight:
                        orientation = GradientDrawable.Orientation.BlTr;
                        break;
                    case GradientColorStackMode.ToBottomLeft:
                        orientation = GradientDrawable.Orientation.TrBl;
                        break;
                    case GradientColorStackMode.ToBottomRight:
                        orientation = GradientDrawable.Orientation.TlBr;
                        break;
                }

                var gradient2 = new GradientDrawable(orientation, colors);
                ViewCompat.SetBackground(this, gradient2);
                base.DispatchDraw(canvas);
                return;
            }

            Android.Graphics.LinearGradient gradient;
            switch (Mode)
            {
                default:
                case GradientColorStackMode.ToRight:
                    gradient = new Android.Graphics.LinearGradient(0, 0, Width, 0, colors, null, Android.Graphics.Shader.TileMode.Mirror);
                    break;
                case GradientColorStackMode.ToLeft:
                    gradient = new Android.Graphics.LinearGradient(Width, 0, 0, 0, colors, null, Android.Graphics.Shader.TileMode.Mirror);
                    break;
                case GradientColorStackMode.ToTop:
                    gradient = new Android.Graphics.LinearGradient(0, Height, 0, 0, colors, null, Android.Graphics.Shader.TileMode.Mirror);
                    break;
                case GradientColorStackMode.ToBottom:
                    gradient = new Android.Graphics.LinearGradient(0, 0, 0, Height, colors, null, Android.Graphics.Shader.TileMode.Mirror);
                    break;
                case GradientColorStackMode.ToTopLeft:
                    gradient = new Android.Graphics.LinearGradient(Width, Height, 0, 0, colors, null, Android.Graphics.Shader.TileMode.Mirror);
                    break;
                case GradientColorStackMode.ToTopRight:
                    gradient = new Android.Graphics.LinearGradient(0, Height, Width, 0, colors, null, Android.Graphics.Shader.TileMode.Mirror);
                    break;
                case GradientColorStackMode.ToBottomLeft:
                    gradient = new Android.Graphics.LinearGradient(Width, 0, 0, Height, colors, null, Android.Graphics.Shader.TileMode.Mirror);
                    break;
                case GradientColorStackMode.ToBottomRight:
                    gradient = new Android.Graphics.LinearGradient(0, 0, Width, Height, colors, null, Android.Graphics.Shader.TileMode.Mirror);
                    break;
            }

            var paint = new Android.Graphics.Paint()
            {
                Dither = true,
            };

            paint.SetShader(gradient);
            canvas.DrawPaint(paint);
            base.DispatchDraw(canvas);
        }
    }
}