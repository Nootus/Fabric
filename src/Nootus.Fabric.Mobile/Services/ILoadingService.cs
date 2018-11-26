using Xamarin.Forms;

namespace Nootus.Fabric.Mobile.Services
{
    public interface ILoadingService
    {
        void InitializeLoading(ContentPage loadingPage);
        void InitializeParent();

        void ShowLoading();

        void HideLoading();
    }
}
