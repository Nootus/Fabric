using Nootus.Fabric.Web.Core.Cosmos.Models;
using Nootus.Fabric.Web.Core.Cosmos.Repositories;
using Nootus.Fabric.Web.Security.Cosmos.Models;

namespace Nootus.Fabric.Web.Security.Cosmos.Repositories
{
    public class SecurityDbContext : CosmosDbContext
    {
        public override DatabaseSettings Settings { get; set; } = SecurityAppSettings.ServiceSettings.Database;
    }
}
