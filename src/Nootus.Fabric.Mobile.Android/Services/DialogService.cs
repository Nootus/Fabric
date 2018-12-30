using Android.App;
using Android.Graphics.Drawables;
using Android.Views;
using Nootus.Fabric.Mobile.Droid.Services;
using Nootus.Fabric.Mobile.Dialog;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using System.Collections.Generic;

[assembly: Dependency(typeof(DialogService))]
namespace Nootus.Fabric.Mobile.Droid.Services
{
    public class DialogService : IDialogService
    {
        private Android.Views.View nativeView;
        private DialogView dialogView;
        private Android.App.Dialog dialog;
        private Stack<int> dialogStack = new Stack<int>();

        public void InitializeDialog()
        {
            this.dialogView = new DialogView(this);
            var renderer = dialogView.GetOrCreateRenderer();

            nativeView = renderer.View;

            dialog = new Android.App.Dialog(BaseApplication.MainActivity);
            dialog.RequestWindowFeature((int)WindowFeatures.NoTitle);
            dialog.SetCancelable(false);
            dialog.SetContentView(nativeView);
            Window window = dialog.Window;
            window.SetLayout(ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.MatchParent);
            // window.ClearFlags(WindowManagerFlags.DimBehind);
            window.SetBackgroundDrawable(new ColorDrawable(Android.Graphics.Color.Transparent));
        }

        public void DisplayLoading()
        {
            if (StackDialog())
            {
                dialogView.InitializeLoading();
                dialog.Show();
            }
        }

        public void DisplayAlert(AlertMode mode, string message)
        {
            dialogView.InitializeAlert(mode, message);
            dialog.Window.AddFlags(WindowManagerFlags.DimBehind);
            dialog.Show();
        }

        public async void DisplayToast(AlertMode mode, string message)
        {
            dialogView.InitializeToast(mode, message);
            dialog.Window.ClearFlags(WindowManagerFlags.DimBehind);
            dialog.Show();
            await Task.Delay(2000);
            Hide();
        }

        public void Hide()
        {
            if (PopDialog())
            {
                dialogView.Hide();
                dialog.Hide();
            }
        }

        public void Dismiss()
        {
            dialog.Dismiss();
        }

        private bool StackDialog()
        {
            dialogStack.Push(0);
            return dialogStack.Count == 1;
        }

        private bool PopDialog()
        {
            if(dialogStack.Count > 0)
                dialogStack.Pop();
            return dialogStack.Count == 0;
        }
    }

    internal static class PlatformExtension
    {
        public static IVisualElementRenderer GetOrCreateRenderer(this VisualElement bindable)
        {
            var renderer = Platform.GetRenderer(bindable);
            if (renderer == null)
            {
                renderer = Platform.CreateRendererWithContext(bindable, BaseApplication.MainActivity);
                Platform.SetRenderer(bindable, renderer);
            }
            return renderer;
        }
    }
}
