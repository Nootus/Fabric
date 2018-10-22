using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Nootus.Fabric.Web.Core.Cosmos;
using Nootus.Fabric.Web.Core.Cosmos.Extensions;
using Nootus.Fabric.Web.Security.Cosmos.Repositories;

namespace Nootus.Fabric.Web.Security.Cosmos
{
    public class SecurityCosmosStartup : MicroserviceCosmosStartup<SecurityDbContext>
    {
        public override void ConfigureServices(IServiceCollection services)
        {
            base.ConfigureServices(services);

            services.AddCosmosDb<SecurityDbContext>();

            // caching page claims
            //services.CachePageClaimsRoles();
        }

        public override void ConfigureDependencyInjection(IServiceCollection services)
        {
            // claims transformation
        }

        public override void Configure(IApplicationBuilder app)
        {
            // app.UseAuthentication();
        }
    }
}
