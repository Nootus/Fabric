using Nootus.Fabric.Mobile.Droid.Services;
using Nootus.Fabric.Mobile.NativeServices;
using System.Collections.Generic;
using Xamarin.Forms;

[assembly: Dependency(typeof(SignatureService))]
namespace Nootus.Fabric.Mobile.Droid.Services
{
    public class SignatureService: ISignatureService
    {
        public List<string> GetAppSignatures()
        {
            AppSignatureService appSignature = new AppSignatureService(BaseApplication.MainActivity);
            return appSignature.GetAppSignatures();
        }
    }
}

