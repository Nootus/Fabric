//-------------------------------------------------------------------------------------------------
// <copyright file="MiddlewareExtensions.cs" company="Nootus">
//  Copyright (c) Nootus. All rights reserved.
// </copyright>
// <description>
//  Extension method to invoke Middleware modules
// </description>
//-------------------------------------------------------------------------------------------------
namespace Nootus.Fabric.Web.Core.Extensions
{
    using Microsoft.AspNetCore.Builder;

    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseContextMiddleware(this IApplicationBuilder app)
        {
            return app.UseMiddleware<ContextMiddleware>();
        }
    }
}
