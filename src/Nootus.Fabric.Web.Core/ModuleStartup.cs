//-------------------------------------------------------------------------------------------------
// <copyright file="ModuleStartup.cs" company="Nootus">
//  Copyright (c) Nootus. All rights reserved.
// </copyright>
// <description>
//  This is base startup class. This initializes the database connection and provide abstract methods
// </description>
//-------------------------------------------------------------------------------------------------
namespace Nootus.Fabric.Web.Core
{
    using AutoMapper;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Nootus.Fabric.Web.Core.Common;

    public abstract class ModuleStartup<TContext> : IModuleStartup
        where TContext : DbContext
    {
        protected IConfiguration Configuration { get; set; }

        public virtual void Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public virtual void ConfigureServices(IServiceCollection services)
        {
            services.AddEntityFrameworkSqlServer()
            .AddDbContext<TContext>(options =>
            {
                options.UseSqlServer(SiteSettings.ConnectionString);
            });

            this.ConfigureDependencyInjection(services);
        }

        public abstract void ConfigureDependencyInjection(IServiceCollection services);

        public void ConfigureMapping(IMapperConfigurationExpression config)
        {
            config.AddProfiles(this.GetType().Assembly);
        }

        public virtual void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            // this is not required for all Modules. Those needed will override
        }
    }
}
