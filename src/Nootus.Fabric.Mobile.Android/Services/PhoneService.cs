using Android.App;
using Android.Gms.Auth.Api;
using Android.Gms.Auth.Api.Credentials;
using Android.Gms.Common.Apis;
using Nootus.Fabric.Mobile.Droid.Services;
using Nootus.Fabric.Mobile.NativeServices;
using Xamarin.Forms;
using static Android.Gms.Auth.Api.Credentials.CredentialPickerConfig;

[assembly: Dependency(typeof(PhoneService))]
namespace Nootus.Fabric.Mobile.Droid.Services
{
    public class PhoneService: IPhoneService
    {
        public string GetPhoneNumber()
        {
            RequestHint();
            return "9908199085";
        }

        private void RequestHint()
        {
            GoogleApiClient apiClient = new GoogleApiClient.Builder(AndroidApplication.MainActivity)
                                                    .AddApi(Auth.CREDENTIALS_API)
                                                    .Build();

            CredentialPickerConfig conf = new CredentialPickerConfig.Builder()
                                            .SetPrompt(Prompt.SignIn)
                                            .Build();

            HintRequest hintRequest = new HintRequest.Builder()
                                        .SetHintPickerConfig(conf)
                                        .SetPhoneNumberIdentifierSupported(true)
                                        .Build();

            PendingIntent intent = Auth.CredentialsApi.GetHintPickerIntent(apiClient, hintRequest);

            AndroidApplication.MainActivity.StartIntentSenderForResult(intent.IntentSender, RequestCode.ResolveHint, null, 0, 0, 0);
        }

    }
}