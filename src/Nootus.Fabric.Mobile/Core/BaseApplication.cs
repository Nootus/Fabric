using Autofac;
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
        }

        protected override void OnStart()
        {
            // Handle when your app starts
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
