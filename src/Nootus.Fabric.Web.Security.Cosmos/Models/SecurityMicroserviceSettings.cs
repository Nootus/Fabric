using Nootus.Fabric.Web.Core.Cosmos.Models;

namespace Nootus.Fabric.Web.Security.Cosmos.Models
{
    public class SecurityMicroserviceSettings: MicroserviceCosmosSettings
    {
        public SecurityDocumentTypes DocumentTypes { get; set; } = new SecurityDocumentTypes();
    }
}
