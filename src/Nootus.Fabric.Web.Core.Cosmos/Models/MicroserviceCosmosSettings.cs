using Nootus.Fabric.Web.Core.Models;

namespace Nootus.Fabric.Web.Core.Cosmos.Models
{
    public class MicroserviceCosmosSettings : MicroserviceSettings
    {
        public DatabaseSettings Database { get; } = new DatabaseSettings();
    }
}
