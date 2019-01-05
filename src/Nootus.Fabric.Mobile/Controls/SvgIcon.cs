using System;
using System.IO;
using System.Reflection;
using Autofac;
using Nootus.Fabric.Mobile.Core;
using Nootus.Fabric.Mobile.Settings;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using Xamarin.Forms;
using SKSvg = SkiaSharp.Extended.Svg.SKSvg;

namespace Nootus.Fabric.Mobile.Controls
{
    public class SvgIcon : Frame
    {
        private readonly SKCanvasView canvasView = new SKCanvasView();

        public static readonly BindableProperty ResourceIdProperty = BindableProperty.Create(
            nameof(ResourceId), typeof(string), typeof(SvgIcon), default(string), propertyChanged: RedrawCanvas);

        public string ResourceId
        {
            get => (string)GetValue(ResourceIdProperty);
            set => SetValue(ResourceIdProperty, value);
        }

        public Color Color { get; set; }

        public SvgIcon()
        {
            Padding = new Thickness(0);
            BackgroundColor = Color.Transparent;
            HasShadow = false;
            Content = canvasView;
            canvasView.PaintSurface += CanvasViewOnPaintSurface;

        }

        private static void RedrawCanvas(BindableObject bindable, object oldvalue, object newvalue)
        {
            SvgIcon svgIcon = bindable as SvgIcon;
            svgIcon?.canvasView.InvalidateSurface();
        }

        private void CanvasViewOnPaintSurface(object sender, SKPaintSurfaceEventArgs args)
        {
            SvgIcon.DrawPicture(args.Surface.Canvas, ResourceId, args.Info.Width, args.Info.Height, Color);
        }

        private static void DrawPicture(SKCanvas canvas, string resourceId, double width, double height, Color color)
        {
            canvas.Clear();

            if (string.IsNullOrEmpty(resourceId))
                return;

            Assembly assembly = DependencyInjection.Container.Resolve<Session>().ResourceAssembly;
            using (Stream stream = assembly.GetManifestResourceStream(resourceId))
            {
                SKSvg svg = new SKSvg();
                svg.Load(stream);

                canvas.Translate((float)width / 2, (float)height / 2);

                SKRect bounds = svg.ViewBox;
                float xRatio = (float)width / bounds.Width;
                float yRatio = (float)height / bounds.Height;

                float ratio = Math.Min(xRatio, yRatio);

                canvas.Scale(ratio);
                canvas.Translate(-bounds.MidX, -bounds.MidY);

                SKPaint paint = GetPaint(color);

                canvas.DrawPicture(svg.Picture, paint);
            }
        }

        public static ImageSource GetSvgImageSource(string resourceId, double width, double height, bool aspectRatio = true)
        {
            return GetSvgImageSource(resourceId, width, height, new Color(), aspectRatio);
        }

        public static ImageSource GetSvgImageSource(string resourceId, double width, double height, Color color, bool aspectRatio = true)
        {

            return (SKBitmapImageSource)GetSvgBitmap(resourceId, width, height, color, aspectRatio);
        }

        public static SKBitmap GetSvgBitmap(string resourceId, double width, double height, Color color, bool aspectRatio = true)
        {
            Assembly assembly = DependencyInjection.Container.Resolve<Session>().ResourceAssembly;
            using (Stream stream = assembly.GetManifestResourceStream(resourceId))
            {

                var svg = new SKSvg();
                svg.Load(stream);

                var bitmap = new SKBitmap((int)width, (int)height);
                var canvas = new SKCanvas(bitmap);
                canvas.Clear();

                float svgWidth = svg.Picture.CullRect.Width;
                float svgHeight = svg.Picture.CullRect.Height;
                float sx, sy;

                if (aspectRatio)
                {
                    float canvasMin = Math.Min((float)width, (float)height);
                    float svgMax = Math.Max(svgWidth, svgHeight);
                    sx = sy = canvasMin / svgMax;
                }
                else
                {
                    sx = (float)width / svgWidth;
                    sy = (float)height / svgHeight;
                }
                var matrix = SKMatrix.MakeScale(sx, sy);

                SKPaint paint = GetPaint(color);
                canvas.DrawPicture(svg.Picture, ref matrix, paint);
                return bitmap;
            }
        }

        private static SKPaint GetPaint(Color color)
        {
            if (!color.IsDefault)
            {
                return new SKPaint()
                {
                    ColorFilter = SKColorFilter.CreateBlendMode(color.ToSKColor(), SKBlendMode.SrcIn)
                };
            }
            else
            {
                return null;
            }


        }
    }
}