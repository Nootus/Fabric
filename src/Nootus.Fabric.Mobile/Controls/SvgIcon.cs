using System;
using System.IO;
using System.Reflection;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using Xamarin.Forms;
using SKSvg = SkiaSharp.Extended.Svg.SKSvg;

namespace Nootus.Fabric.Mobile.Controls
{
    public class SvgIcon : Frame
    {
        private readonly SKCanvasView canvasView = new SKCanvasView();
        private readonly Assembly iconAssembly;

        public static readonly BindableProperty ResourceIdProperty = BindableProperty.Create(
            nameof(ResourceId), typeof(string), typeof(SvgIcon), default(string), propertyChanged: RedrawCanvas);

        public string ResourceId
        {
            get => (string)GetValue(ResourceIdProperty);
            set => SetValue(ResourceIdProperty, value);
        }

        public Color Color
        {
            get; set;
        }

        public SvgIcon()
        {
            Padding = new Thickness(0);
            BackgroundColor = Color.Transparent;
            HasShadow = false;
            Content = canvasView;
            canvasView.PaintSurface += CanvasViewOnPaintSurface;
            iconAssembly = Assembly.GetCallingAssembly();
            
        }

        private static void RedrawCanvas(BindableObject bindable, object oldvalue, object newvalue)
        {
            SvgIcon svgIcon = bindable as SvgIcon;
            svgIcon?.canvasView.InvalidateSurface();
        }

        private void CanvasViewOnPaintSurface(object sender, SKPaintSurfaceEventArgs args)
        {
            //SKCanvas canvas = args.Surface.Canvas;
            //canvas.Clear();

            //if (string.IsNullOrEmpty(ResourceId))
            //    return;

            //using (Stream stream = iconAssembly.GetManifestResourceStream(ResourceId))
            //{
            //    SKSvg svg = new SKSvg();
            //    svg.Load(stream);

            //    SKImageInfo info = args.Info;
            //    canvas.Translate(info.Width / 2f, info.Height / 2f);

            //    SKRect bounds = svg.ViewBox;
            //    float xRatio = info.Width / bounds.Width;
            //    float yRatio = info.Height / bounds.Height;

            //    float ratio = Math.Min(xRatio, yRatio);

            //    canvas.Scale(ratio);
            //    canvas.Translate(-bounds.MidX, -bounds.MidY);

            //    var paint = new SKPaint()
            //    {
            //        ColorFilter = SKColorFilter.CreateBlendMode(Color.ToSKColor(), SKBlendMode.SrcIn)
            //    };

            //    canvas.DrawPicture(svg.Picture, paint);                
            //}

            SvgIcon.DrawPicture(args.Surface.Canvas, iconAssembly, ResourceId, args.Info.Width, args.Info.Height, Color);
        }

        private static void DrawPicture(SKCanvas canvas, Assembly assembly, string resourceId, int width, int height, Color color)
        {
            canvas.Clear();

            if (string.IsNullOrEmpty(resourceId))
                return;

            using (Stream stream = assembly.GetManifestResourceStream(resourceId))
            {
                SKSvg svg = new SKSvg();
                svg.Load(stream);

                canvas.Translate(width / 2f, height / 2f);

                SKRect bounds = svg.ViewBox;
                float xRatio = width / bounds.Width;
                float yRatio = height / bounds.Height;

                float ratio = Math.Min(xRatio, yRatio);

                canvas.Scale(ratio);
                canvas.Translate(-bounds.MidX, -bounds.MidY);

                var paint = new SKPaint()
                {
                    ColorFilter = SKColorFilter.CreateBlendMode(color.ToSKColor(), SKBlendMode.SrcIn)
                };

                canvas.DrawPicture(svg.Picture, paint);
            }
        }

        public static ImageSource GetSvgImageSource(Assembly assembly, string resourceId, int width, int height, Color color)
        {
            var scaleFactor = 0;

#if __IOS__
                scaleFactor = (int)UIKit.UIScreen.MainScreen.Scale;
#elif __ANDROID__
                 //I have added a static Current property to my MainActivity.
                 scaleFactor = MainApplication.CurrentContext.Resources.DisplayMetrics.Density
#endif

            var bitmap = new SKBitmap((int)(width * scaleFactor), (int)(height * scaleFactor));
            var canvas = new SKCanvas(bitmap);

            DrawPicture(canvas, assembly, resourceId, width, height, color);

            var image = SKImage.FromBitmap(bitmap);
            var encoded = image.Encode();
            var stream = encoded.AsStream();
            var source = ImageSource.FromStream(() => stream);

            return source;
        }
    }
}