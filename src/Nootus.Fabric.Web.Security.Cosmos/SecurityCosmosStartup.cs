using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Nootus.Fabric.Web.Core.Cosmos;
using Nootus.Fabric.Web.Core.Cosmos.Extensions;
using Nootus.Fabric.Web.Core.Cosmos.Models;
using Nootus.Fabric.Web.Security.Core.Domain;
using Nootus.Fabric.Web.Security.Cosmos.Common;
using Nootus.Fabric.Web.Security.Cosmos.Domain;
using Nootus.Fabric.Web.Security.Cosmos.Models;
using Nootus.Fabric.Web.Security.Cosmos.Repositories;
using System.Text;
using System.Threading.Tasks;

namespace Nootus.Fabric.Web.Security.Cosmos
{
    public class SecurityCosmosStartup : MicroserviceCosmosStartup<SecurityDbContext>
    {
        public override void Startup(IConfiguration configuration)
        {
            base.Startup(configuration);

            // initializing configuration
            SecurityMicroserviceSettings.ServiceSettings.ServiceName = Configuration.GetValue<string>("Microservices:Security:Name");

            // Token settings
            TokenSettings.SymmetricKey = Configuration.GetValue<string>("Microservices:Security:JWT:SymmetricKey");
            TokenSettings.Issuer = Configuration.GetValue<string>("Microservices:Security:JWT:Issuer");
            TokenSettings.Duration = Configuration.GetValue<int>("Microservices:Security:JWT:Duration");

            // Cosmos settings
            DatabaseSettings dbSettings = SecurityMicroserviceSettings.ServiceSettings.Database;
            dbSettings.Endpoint = Configuration.GetValue<string>("Microservices:Security:Database:Endpoint");
            dbSettings.Key = Configuration.GetValue<string>("Microservices:Security:Database:Key");
            dbSettings.DatabaseId = Configuration.GetValue<string>("Microservices:Security:Database:DatabaseId");
            dbSettings.CollectionId = Configuration.GetValue<string>("Microservices:Security:Database:CollectionId");
        }

        public override void ConfigureServices(IServiceCollection services)
        {
            base.ConfigureServices(services);

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = TokenService.GetTokenValidationParameters();
                    options.Events = new JwtBearerEvents
                    {
                        OnAuthenticationFailed = context =>
                        {
                            if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                            {
                                context.Response.Headers.Add("Token-Expired", "true");
                            }
                            return Task.CompletedTask;
                        }
                    };
                });

            services.AddCosmosDb<SecurityDbContext>();

            // caching page claims
            //services.CachePageClaimsRoles();
        }

        public override void ConfigureDependencyInjection(IServiceCollection services)
        {
            services.AddTransient<IAccountDomain, AccountDomain>();
            services.AddTransient<AccountRepository>();
        }

        public override void Configure(IApplicationBuilder app)
        {
            app.UseAuthentication();
        }
    }
}
