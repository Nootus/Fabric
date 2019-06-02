using Android.Graphics;
using Android.Widget;
using Nootus.Fabric.Mobile.Droid.Effects;
using Nootus.Fabric.Mobile.Behaviors;
using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Nootus.Fabric.Mobile.Effects;
using System.Linq;

[assembly: ResolutionGroupName("Nootus")]
[assembly: ExportEffect(typeof(EntryLineColorEffect), "EntryLineColorEffect")]
namespace Nootus.Fabric.Mobile.Droid.Effects
{
    public class EntryLineColorEffect : PlatformEffect
    {
        //EditText control;

        protected override void OnAttached()
        {
            //control = Control as EditText;
            UpdateLineColor();
        }

        protected override void OnDetached()
        {
            //control = null;
        }

        protected override void OnElementPropertyChanged(PropertyChangedEventArgs args)
        {
            if (args.PropertyName == LineColorBehavior.LineColorProperty.PropertyName)
            {
                UpdateLineColor();
            }
        }

        private void UpdateLineColor()
        {
            //if (control != null)
            //{
                var effect = (LineColorEffect)Element.Effects.FirstOrDefault(e => e is LineColorEffect);
                if (effect != null)
                {
                    Control.Background.SetColorFilter(effect.Color.ToAndroid(), PorterDuff.Mode.SrcAtop);
                }

                //control.Background.SetColorFilter(LineColorBehavior.GetLineColor(Element).ToAndroid(), PorterDuff.Mode.SrcAtop);
            //}
        }
    }
}
