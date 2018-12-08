using Xamarin.Forms;

namespace Nootus.Fabric.Mobile.Dialog
{
    public interface IDialogService
    {
        void InitializeDialog();
        void DisplayLoading();
        void DisplayAlert(AlertMode mode, string message);
        void DisplayToast(AlertMode mode, string message);
        void Hide();
    }
}
