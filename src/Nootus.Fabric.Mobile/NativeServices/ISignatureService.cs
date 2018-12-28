using System.Collections.Generic;

namespace Nootus.Fabric.Mobile.NativeServices
{
    public interface ISignatureService
    {
        List<string> GetAppSignatures();
    }
}
