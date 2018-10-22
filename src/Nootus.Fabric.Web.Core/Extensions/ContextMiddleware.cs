//-------------------------------------------------------------------------------------------------
// <copyright file="ContextMiddleware.cs" company="Nootus">
//  Copyright (c) Nootus. All rights reserved.
// </copyright>
// <description>
//  Middleware used to store HttpContext
// </description>
//-------------------------------------------------------------------------------------------------
namespace Nootus.Fabric.Web.Core.Extensions
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;
    using Nootus.Fabric.Web.Core.Context;

    public class ContextMiddleware
    {
        private readonly RequestDelegate next;

        public ContextMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            NTContext.HttpContext = context;
            NTContext.Context = null;
            await this.next(context);
        }
    }
}
