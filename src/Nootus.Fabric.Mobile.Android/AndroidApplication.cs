using Android.App;
using Android.OS;
using Android.Runtime;
using System;

namespace Nootus.Fabric.Mobile.Droid
{
    public class AndroidApplication : Application, Application.IActivityLifecycleCallbacks
    {
        public static Activity MainActivity { get; private set; }

        public AndroidApplication(IntPtr handle, JniHandleOwnership transer)
          : base(handle, transer)
        {
        }

        public override void OnCreate()
        {
            base.OnCreate();
            RegisterActivityLifecycleCallbacks(this);
        }

        public override void OnTerminate()
        {
            base.OnTerminate();
            UnregisterActivityLifecycleCallbacks(this);
        }

        public void OnActivityCreated(Activity activity, Bundle savedInstanceState)
        {
            MainActivity = activity;
            var scaleFactor = activity.Resources.DisplayMetrics.Density;
        }

        public void OnActivityDestroyed(Activity activity)
        {
        }

        public void OnActivityPaused(Activity activity)
        {
        }

        public void OnActivityResumed(Activity activity)
        {
            MainActivity = activity;
        }

        public void OnActivitySaveInstanceState(Activity activity, Bundle outState)
        {
        }

        public void OnActivityStarted(Activity activity)
        {
            MainActivity = activity;
        }

        public void OnActivityStopped(Activity activity)
        {
        }
    }
}
