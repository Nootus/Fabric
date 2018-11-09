using Autofac;
using Autofac.Core;

namespace Nootus.Fabric.Mobile.Core
{
    public static class DependencyInjection
    {
        public static bool IsBuilt { get; set; } = false;

        public static ContainerBuilder Builder { get; } = new ContainerBuilder();

        public static IContainer Container { get; set; }
    }
}
