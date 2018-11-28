namespace Nootus.Fabric.Mobile.Services
{
    public interface IToastService
    {
        void LongToast(string message);
        void ShortToast(string message);
    }
}
