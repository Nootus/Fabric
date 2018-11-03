using Autofac;
using Autofac.Core;
using Xamarin.Forms;

namespace Nootus.Fabric.Mobile.Core
{
    public class MobileApplication: Application
    {
        public static Container Container;
        public MobileApplication()
        {
            Initialize();
        }

        protected virtual void Initialize()
        {
            ConfigureDependencyInjection();
        }

        private void ConfigureDependencyInjection()
        {
            ConfigureDependencyInjection(DependencyInjection.Builder);
            DependencyInjection.Builder.Build();
        }

        protected virtual void ConfigureDependencyInjection(ContainerBuilder builder)
        {
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
