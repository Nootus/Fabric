using System;

namespace Nootus.Fabric.Mobile.NativeServices
{
    public interface IPhoneService
    {
        void GetPhoneNumber(Action<string> callback);
    }
}
