//-------------------------------------------------------------------------------------------------
// <copyright file="MiddlewareExtensions.cs" company="Nootus">
//  Copyright (c) Nootus. All rights reserved.
// </copyright>
// <description>
//  Extension method for the profile middleware
// </description>
//-------------------------------------------------------------------------------------------------
namespace Nootus.Fabric.Web.Security.Extensions
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.DependencyInjection;
    using Nootus.Fabric.Web.Security.Middleware;
    using Nootus.Fabric.Web.Security.Repositories;

    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseProfileMiddleware(this IApplicationBuilder app)
        {
            return app.UseMiddleware<ProfileMiddleware>();
        }

        public static IServiceCollection CachePageClaimsRoles(this IServiceCollection services)
        {
            SecurityRepository repository = services.BuildServiceProvider().GetRequiredService<SecurityRepository>();
            PageService.CachePageClaimsRoles(repository);
            return services;
        }
    }
}
