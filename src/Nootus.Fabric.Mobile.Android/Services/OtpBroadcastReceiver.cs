
using Android.App;
using Android.Content;
using Android.Gms.Auth.Api.Phone;
using Android.Gms.Common.Apis;
using Android.OS;

namespace Nootus.Fabric.Mobile.Droid.Services
{
    [BroadcastReceiver(Enabled = true)]
    [IntentFilter(new[] { SmsRetriever.SmsRetrievedAction })]
    public class OtpBroadcastReceiver : BroadcastReceiver
    {
        public override void OnReceive(Context context, Intent intent)
        {
            Bundle extras = intent.Extras;

            Statuses status = (Statuses) extras.Get(SmsRetriever.ExtraStatus);
            switch (status.StatusCode)
            {
                case CommonStatusCodes.Success:
                    string message = (string)extras.Get(SmsRetriever.ExtraSmsMessage);
                    BaseApplication.MainActivity.OtpCallback(message);
                    break;
                case CommonStatusCodes.Timeout:
                    break;
            }
        }
    }
}
