using Xamarin.Forms;

namespace Nootus.Fabric.Mobile.Services
{
    public interface ILoadingService
    {
        void InitializeLoading(ContentPage loadingPage);
        void ShowLoading();
        void HideLoading();
    }
}
