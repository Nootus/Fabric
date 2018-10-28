using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nootus.Fabric.Web.Core.Cosmos;
using Nootus.Fabric.Web.Core.Cosmos.Models;
using Nootus.Fabric.Web.Security.Core;
using Nootus.Fabric.Web.Security.Core.Domain;
using Nootus.Fabric.Web.Security.Cosmos.Domain;
using Nootus.Fabric.Web.Security.Cosmos.Models;
using Nootus.Fabric.Web.Security.Cosmos.Repositories;

namespace Nootus.Fabric.Web.Security.Cosmos
{
    public class SecurityCosmosStartup : MicroserviceCosmosStartup<SecurityDbContext>
    {
        public override void Startup(IConfiguration configuration)
        {
            base.Startup(configuration);

            // initializing configuration
            SecurityAppSettings.ServiceSettings.ServiceName = Configuration.GetValue<string>("Microservices:Security:Name");

            // Token configuration
            SecurityStartup.ConfigureTokenSettings(configuration);

            // Cosmos settings
            DatabaseSettings dbSettings = SecurityAppSettings.ServiceSettings.Database;
            dbSettings.Endpoint = Configuration.GetValue<string>("Microservices:Security:Database:Endpoint");
            dbSettings.Key = Configuration.GetValue<string>("Microservices:Security:Database:Key");
            dbSettings.DatabaseId = Configuration.GetValue<string>("Microservices:Security:Database:DatabaseId");
            dbSettings.CollectionId = Configuration.GetValue<string>("Microservices:Security:Database:CollectionId");

            // Document Types
            SecurityDocumentTypes documentTypes = SecurityAppSettings.ServiceSettings.DocumentTypes;
            documentTypes.UserProfile = Configuration.GetValue<string>("Microservices:Security:Database:DocumentTypes:UserProfile");
            documentTypes.UserAuth = Configuration.GetValue<string>("Microservices:Security:Database:DocumentTypes:UserAuth");
            documentTypes.Role = Configuration.GetValue<string>("Microservices:Security:Database:DocumentTypes:Role");
            documentTypes.Claim = Configuration.GetValue<string>("Microservices:Security:Database:DocumentTypes:Claim");
            documentTypes.Page = Configuration.GetValue<string>("Microservices:Security:Database:DocumentTypes:Page");
        }

        public override void ConfigureServices(IServiceCollection services)
        {
            base.ConfigureServices(services);

            SecurityStartup.ConfigureTokenServices(services);

            // caching page claims
            //services.CachePageClaimsRoles();
        }

        public override void ConfigureDependencyInjection(IServiceCollection services)
        {
            base.ConfigureDependencyInjection(services);

            services.AddTransient<IAccountService, AccountService>();
        }

        public override void Configure(IApplicationBuilder app)
        {
            app.UseAuthentication();
        }
    }
}
