//-------------------------------------------------------------------------------------------------
// <copyright file="MicroserviceCosmosStartup.cs" company="Nootus">
//  Copyright (c) Nootus. All rights reserved.
// </copyright>
// <description>
//  This is base startup class. This initializes the cosmos database connection and provide abstract methods
// </description>
//-------------------------------------------------------------------------------------------------
namespace Nootus.Fabric.Web.Core.Cosmos
{
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Nootus.Fabric.Web.Core.Cosmos.Extensions;
    using Nootus.Fabric.Web.Core.Cosmos.Models;
    using Nootus.Fabric.Web.Core.Cosmos.Repositories;

    public abstract class MicroserviceCosmosStartup<TContext>: MicroserviceStartup
        where TContext : CosmosDbContext
    {
        public override void ConfigureServices(IServiceCollection services)
        {
            services.AddCosmosDb<TContext>();
            base.ConfigureServices(services);
        }
    }
}
