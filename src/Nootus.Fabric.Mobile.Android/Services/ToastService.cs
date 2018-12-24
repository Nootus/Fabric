using Android.App;
using Android.Widget;
using Nootus.Fabric.Mobile.Droid.Services;
using Nootus.Fabric.Mobile.Services;

[assembly: Xamarin.Forms.Dependency(typeof(ToastService))]
namespace Nootus.Fabric.Mobile.Droid.Services
{
    public class ToastService : IToastService
    {
        public void LongToast(string message)
        {
            Toast.MakeText(Application.Context, message, Android.Widget.ToastLength.Long).Show();
        }

        public void ShortToast(string message)
        {
            Toast.MakeText(Application.Context, message, Android.Widget.ToastLength.Short).Show();
        }
    }
}