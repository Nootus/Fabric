using Android.Content;
using Nootus.Fabric.Mobile.Droid.Renderers;
using Nootus.Fabric.Mobile.Views;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using AView = Android.Views.View;


[assembly: ExportRenderer(typeof(BaseNavigationPage), typeof(BaseNavigationRenderer))]
namespace Nootus.Fabric.Mobile.Droid.Renderers
{
    public class BaseNavigationRenderer: NavigationRenderer
    {
        public BaseNavigationRenderer(Context context): base(context) { }

        IPageController PageController => Element as IPageController;
        BaseNavigationPage CustomNavigationPage => Element as BaseNavigationPage;

        protected override void OnLayout(bool changed, int l, int t, int r, int b)
        {
            CustomNavigationPage.IgnoreLayoutChange = true;
            base.OnLayout(changed, l, t, r, b);
            CustomNavigationPage.IgnoreLayoutChange = false;

            int containerHeight = b - t;

            PageController.ContainerArea = new Rectangle(0, 0, Context.FromPixels(r - l), Context.FromPixels(containerHeight));

            for (var i = 0; i < ChildCount; i++)
            {
                AView child = GetChildAt(i);

                if (child is Android.Support.V7.Widget.Toolbar)
                {
                    continue;
                }

                child.Layout(0, 0, r, b);
            }
        }
    }
}