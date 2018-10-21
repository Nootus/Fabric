//-------------------------------------------------------------------------------------------------
// <copyright file="MiddlewareExtensions.cs" company="Nootus">
//  Copyright (c) Nootus. All rights reserved.
// </copyright>
// <description>
//  Extension method for the profile middleware
// </description>
//-------------------------------------------------------------------------------------------------
namespace Nootus.Fabric.Web.Security.Core.Extensions
{
    using Microsoft.AspNetCore.Builder;
    using Nootus.Fabric.Web.Security.Core.Middleware;

    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseProfileMiddleware(this IApplicationBuilder app)
        {
            return app.UseMiddleware<ProfileMiddleware>();
        }
    }
}
