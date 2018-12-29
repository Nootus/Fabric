using Android.App;
using Android.Gms.Auth.Api;
using Android.Gms.Auth.Api.Credentials;
using Android.Gms.Common.Apis;
using Android.Gms.Tasks;
using Com.Google.Android.Gms.Auth.Api.Phone;
using Nootus.Fabric.Mobile.Dialog;
using Nootus.Fabric.Mobile.Droid.Services;
using Nootus.Fabric.Mobile.NativeServices;
using System;
using System.Text.RegularExpressions;
using Xamarin.Forms;
using static Android.Gms.Auth.Api.Credentials.CredentialPickerConfig;

[assembly: Dependency(typeof(PhoneService))]
namespace Nootus.Fabric.Mobile.Droid.Services
{
    public class PhoneService: IPhoneService
    {
        public void GetPhoneNumber(Action<string> callback)
        {
            DependencyService.Get<IDialogService>().Dismiss(); // work around Loading Dialog and Phone Dialog
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

        public void RetrieveOtp(Action<string> callback)
        {
            SmsRetrieverClient client = SmsRetriever.GetClient(BaseApplication.MainActivity);
            BaseApplication.MainActivity.OtpCallback = message => {
                string otp = Regex.Match(message, @"\d{6}")?.Value;
                callback(otp);
            };

            Task task = client.StartSmsRetriever();
        }
    }
}
