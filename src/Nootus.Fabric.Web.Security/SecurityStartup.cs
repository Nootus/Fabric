//-------------------------------------------------------------------------------------------------
// <copyright file="SecurityStartup.cs" company="Nootus">
//  Copyright (c) Nootus. All rights reserved.
// </copyright>
// <description>
//  Startup class for the security module
// </description>
//-------------------------------------------------------------------------------------------------
namespace Nootus.Fabric.Web.Security
{
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;
    using Nootus.Fabric.Web.Core;
    using Nootus.Fabric.Web.Security.Domain;
    using Nootus.Fabric.Web.Security.Entities;
    using Nootus.Fabric.Web.Security.Extensions;
    using Nootus.Fabric.Web.Security.Identity;
    using Nootus.Fabric.Web.Security.Repositories;

    public class SecurityStartup : ModuleStartup<SecurityDbContext>
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

        public override void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseAuthentication();
        }
    }
}