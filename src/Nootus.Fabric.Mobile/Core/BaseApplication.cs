using Autofac;
using Nootus.Fabric.Mobile.Settings;
using Nootus.Fabric.Mobile.WebApi;
using Xamarin.Forms;

namespace Nootus.Fabric.Mobile.Core
{
    public class BaseApplication: Application
    {
        public BaseApplication()
        {
            Initialize();
        }

        protected virtual void Initialize()
        {
            ConfigureDependencyInjection();
        }

        private void ConfigureDependencyInjection()
        {
            if(!DependencyInjection.IsBuilt)
            {
                ConfigureDependencyInjection(DependencyInjection.Builder);
                DependencyInjection.Container = DependencyInjection.Builder.Build();
                DependencyInjection.IsBuilt = true;
            }
        }

        protected virtual void ConfigureDependencyInjection(ContainerBuilder builder)
        {
            builder.RegisterType<UserProfile>().SingleInstance();
            builder.RegisterType<SettingsService>().SingleInstance();
            builder.RegisterType<ApiRequest>().SingleInstance();
            builder.RegisterType<Session>().SingleInstance();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }

    }
}
