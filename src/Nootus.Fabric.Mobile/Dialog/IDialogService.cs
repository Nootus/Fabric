using Xamarin.Forms;

namespace Nootus.Fabric.Mobile.Dialog
{
    public interface IDialogService
    {
        void InitializeLoading();
        void ShowLoading();
        void Hide();
    }
}
