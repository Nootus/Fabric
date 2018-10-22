//-------------------------------------------------------------------------------------------------
// <copyright file="SecurityStartup.cs" company="Nootus">
//  Copyright (c) Nootus. All rights reserved.
// </copyright>
// <description>
//  Startup class for the security module
// </description>
//-------------------------------------------------------------------------------------------------
namespace Nootus.Fabric.Web.Security.SqlServer
{
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;
    using Nootus.Fabric.Web.Core.SqlServer;
    using Nootus.Fabric.Web.Security.Core.Identity;
    using Nootus.Fabric.Web.Security.SqlServer.Domain;
    using Nootus.Fabric.Web.Security.SqlServer.Entities;
    using Nootus.Fabric.Web.Security.SqlServer.Identity;
    using Nootus.Fabric.Web.Security.SqlServer.Middleware;
    using Nootus.Fabric.Web.Security.SqlServer.Repositories;

    public class SecuritySqlServerStartup : MicroserviceSqlServerStartup<SecurityDbContext>
    {
        public override void ConfigureServices(IServiceCollection services)
        {
            base.ConfigureServices(services);

            services.AddIdentity<ApplicationUser, ApplicationRole>()
            .AddEntityFrameworkStores<SecurityDbContext>()
            .AddDefaultTokenProviders();

            // caching page claims
            services.CachePageClaimsRoles();
        }

        public override void ConfigureDependencyInjection(IServiceCollection services)
        {
            // claims transformation
            services.AddSingleton<IClaimsTransformation, ClaimsTransformer>();

            services.AddTransient<AccountDomain>();
            services.AddTransient<SecurityRepository>();
            services.AddScoped<SignInManager<ApplicationUser>, ApplicationSignInManager>();
        }

        public override void Configure(IApplicationBuilder app)
        {
            app.UseAuthentication();
        }
    }
}