using Android.App;
using Android.OS;
using Android.Runtime;
using System;

namespace Nootus.Fabric.Mobile.Droid
{
    public class BaseApplication : Application, Application.IActivityLifecycleCallbacks
    {
        public static BaseActivity MainActivity { get; private set; }

        public BaseApplication(IntPtr handle, JniHandleOwnership transer)
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
            MainActivity = (BaseActivity) activity;
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
            MainActivity = (BaseActivity) activity;
        }

        public void OnActivitySaveInstanceState(Activity activity, Bundle outState)
        {
        }

        public void OnActivityStarted(Activity activity)
        {
            MainActivity = (BaseActivity) activity;
        }

        public void OnActivityStopped(Activity activity)
        {
        }
    }
}
