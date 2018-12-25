using Android.App;
using Android.Gms.Auth.Api;
using Android.Gms.Auth.Api.Credentials;
using Android.Gms.Common.Apis;
using Nootus.Fabric.Mobile.Droid.Services;
using Nootus.Fabric.Mobile.NativeServices;
using System;
using Xamarin.Forms;
using static Android.Gms.Auth.Api.Credentials.CredentialPickerConfig;

[assembly: Dependency(typeof(PhoneService))]
namespace Nootus.Fabric.Mobile.Droid.Services
{
    public class PhoneService: IPhoneService
    {
        public void GetPhoneNumber(Action<string> callback)
        {
            RequestHint(callback);
        }

        private void RequestHint(Action<string> callback)
        {
            GoogleApiClient apiClient = new GoogleApiClient.Builder(BaseApplication.MainActivity)
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

            BaseApplication.MainActivity.StartIntentSender(intent.IntentSender, RequestCode.ResolveHint, callback);
        }

    }
}