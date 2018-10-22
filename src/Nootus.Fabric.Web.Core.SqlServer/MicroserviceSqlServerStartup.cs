//-------------------------------------------------------------------------------------------------
// <copyright file="ModuleStartup{TContext}.cs" company="Nootus">
//  Copyright (c) Nootus. All rights reserved.
// </copyright>
// <description>
//  This is base startup class. This initializes the database connection and provide abstract methods
// </description>
//-------------------------------------------------------------------------------------------------
namespace Nootus.Fabric.Web.Core.SqlServer
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Nootus.Fabric.Web.Core.Common;

    public abstract class MicroserviceSqlServerStartup<TContext> : MicroserviceStartup
        where TContext : DbContext
    {
        public override void ConfigureServices(IServiceCollection services)
        {
            services.AddEntityFrameworkSqlServer()
            .AddDbContext<TContext>(options =>
            {
                options.UseSqlServer(FabricSettings.ConnectionString);
            });

            base.ConfigureServices(services);
        }
    }
}
