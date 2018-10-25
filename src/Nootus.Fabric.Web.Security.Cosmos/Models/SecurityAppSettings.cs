using Nootus.Fabric.Web.Core.Cosmos.Models;

namespace Nootus.Fabric.Web.Security.Cosmos.Models
{
    public static class SecurityAppSettings
    {
        public static SecurityMicroserviceSettings ServiceSettings { get; } = new SecurityMicroserviceSettings();
    }
}
