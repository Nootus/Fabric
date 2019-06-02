using Xamarin.Forms;

namespace Nootus.Fabric.Mobile.Effects
{
    public class LineColorEffect : RoutingEffect
    {
        public LineColorEffect() : base("Nootus.EntryLineColorEffect")
        {
        }

        public Color Color { get; set; } = Color.Default;
    }
}
