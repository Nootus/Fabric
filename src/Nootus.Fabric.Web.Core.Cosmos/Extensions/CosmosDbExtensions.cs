//-------------------------------------------------------------------------------------------------
// <copyright file="CosmosDbMiddlewareExtensions.cs" company="Nootus">
//  Copyright (c) Nootus. All rights reserved.
// </copyright>
// <description>
//  Extension method to invoke Cosmos Middleware modules
// </description>
//-------------------------------------------------------------------------------------------------
namespace Nootus.Fabric.Web.Core.Cosmos.Extensions
{
    using Microsoft.Extensions.DependencyInjection;
    using Nootus.Fabric.Web.Core.Cosmos.Repositories;

    public static class CosmosDbExtensions
    {
        public static void AddCosmosDb<TContext>(this IServiceCollection services)
            where TContext: CosmosDbContext
        {
            services.AddSingleton<TContext>();
            TContext context = services.BuildServiceProvider().GetRequiredService<TContext>();
            context.Initialize();
        }
    }
}
