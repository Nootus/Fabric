using Autofac;
using Autofac.Core;

namespace Nootus.Fabric.Mobile.Core
{
    public static class DependencyInjection
    {
        public static ContainerBuilder Builder { get; } = new ContainerBuilder();

        public static IContainer Container { get; private set; }
    }
}
