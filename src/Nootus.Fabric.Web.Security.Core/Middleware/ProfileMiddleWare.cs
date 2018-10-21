//-------------------------------------------------------------------------------------------------
// <copyright file="ProfileMiddleWare.cs" company="Nootus">
//  Copyright (c) Nootus. All rights reserved.
// </copyright>
// <description>
//  Middleware to set the profile onto the CallingContext
// </description>
//-------------------------------------------------------------------------------------------------
namespace Nootus.Fabric.Web.Security.Core.Middleware
{
    using Microsoft.AspNetCore.Http;
    using Nootus.Fabric.Web.Core.Common;
    using Nootus.Fabric.Web.Core.Context;
    using Nootus.Fabric.Web.Security.Core.Common;
    using Nootus.Fabric.Web.Security.Core.Identity;
    using System;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;

    public class ProfileMiddleware
    {
        private readonly RequestDelegate next;

        public ProfileMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            if (context.User.Identity.IsAuthenticated)
            {
                await this.SetNTContext(context);
            }
            else
            {
                // automatically logging in in the dev mode
                await this.LoginDevEnvironment();
            }

            await this.next(context);
        }

        public async Task SetNTContext(HttpContext context)
        {
            var claims = context.User.Claims;

            string companyId = context.Request.Headers[SecurityConstants.HeaderCompanyId];
            companyId = companyId ?? claims.Where(c => c.Type == NTClaimTypes.CompanyId).Select(c => c.Value).FirstOrDefault();
            var userId = claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            if (userId is null)
            {
                await this.LoginDevEnvironment();
            }

            NTContextModel model = new NTContextModel()
            {
                UserId = userId?.Value,
                UserName = context.User.Identity.Name,
                FirstName = claims.First(c => c.Type == NTClaimTypes.FirstName).Value,
                LastName = claims.First(c => c.Type == NTClaimTypes.LastName).Value,
                CompanyId = Convert.ToInt32(companyId ?? "0"),
            };

            // setting the Group CompanyId
            //model.GroupCompanyId = PageService.CompanyClaims[model.CompanyId]?.ParentCompanyId ?? model.CompanyId;

            NTContext.Context = model;
        }

        private async Task LoginDevEnvironment()
        {
            if (FabricSettings.LoginDevEnvironment && FabricSettings.IsEnvironment(SecurityConstants.DevEnvironment))
            {
                //ApplicationUser user = await this.userManager.FindByNameAsync(SecuritySettings.NootusProfileUserName);
                //await this.signInManager.SignInAsync(user, false);
                await Task.CompletedTask;
            }
        }
    }
}