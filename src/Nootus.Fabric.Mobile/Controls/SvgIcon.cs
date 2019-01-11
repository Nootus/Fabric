using System;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using Autofac;
using Nootus.Fabric.Mobile.Core;
using Nootus.Fabric.Mobile.Settings;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using Xamarin.Forms;
using SKSvg = SkiaSharp.Extended.Svg.SKSvg;

namespace Nootus.Fabric.Mobile.Controls
{
    public static class SvgIcon
    {
        public static ImageSource GetSvgImageSource(string resourceId, bool aspectRatio = true)
        {
            return GetSvgImageSource(resourceId, 0, 0, new Color(), aspectRatio);
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
            return DrawPicture(resourceId, width, height, color, aspectRatio);
        }


        private static SKBitmap DrawPicture(string resourceId, double width, double height, Color color, bool aspectRatio = true)
        {
            Assembly assembly = DependencyInjection.Container.Resolve<Session>().ResourceAssembly;
            using (Stream stream = assembly.GetManifestResourceStream(resourceId))
            {

                var svg = new SKSvg();
                svg.Load(stream);

                float svgWidth = svg.Picture.CullRect.Width;
                float svgHeight = svg.Picture.CullRect.Height;
                float sx, sy;

                width = width <= 0 ? svgWidth : width;
                height = height <= 0 ? svgHeight : height;

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

                SKBitmap bitmap = new SKBitmap((int)width, (int)height);
                SKCanvas canvas = new SKCanvas(bitmap);
                canvas.Clear();

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