using Xamarin.Forms;

namespace Nootus.Fabric.Mobile.Services
{
    public interface ILoadingService
    {
        void InitLoading(ContentPage loadingIndicatorPage = null);

        void ShowLoading();

        void HideLoading();
    }
}
