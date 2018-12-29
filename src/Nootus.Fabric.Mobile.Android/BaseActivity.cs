using Android.App;
using Android.Content;
using Android.Gms.Auth.Api.Credentials;
using Nootus.Fabric.Mobile.Droid.Services;
using System;
using Xamarin.Forms;

namespace Nootus.Fabric.Mobile.Droid
{
    public class BaseActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        private Action<string> phoneNumberCallback;
        public Action<string> OtpCallback { get; set; }

        public void StartIntentSender<TCallBack>(IntentSender intent, int requestCode, Action<TCallBack> callback)
            where TCallBack: class
        {
            if (requestCode == RequestCode.ResolveHint)
            {
                phoneNumberCallback = (Action<string>) callback;
            }

            StartIntentSenderForResult(intent, requestCode, null, 0, 0, 0);
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            if (requestCode == RequestCode.ResolveHint)
            {
                if (resultCode == Result.Ok)
                {
                    Credential credential = (Credential)data.GetParcelableExtra(Credential.ExtraKey);
                    string phonenumber = credential?.Id;
                    phoneNumberCallback(phonenumber);
                }
            }
        }
    }
}
