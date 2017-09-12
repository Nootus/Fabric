//-------------------------------------------------------------------------------------------------
// <copyright file="ProfileMiddleWare.cs" company="Nootus">
//  Copyright (c) Nootus. All rights reserved.
// </copyright>
// <description>
//  Middleware to set the profile onto the CallingContext
// </description>
//-------------------------------------------------------------------------------------------------
namespace Nootus.Fabric.Web.Security.Middleware
{
    using System;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Nootus.Fabric.Web.Core.Common;
    using Nootus.Fabric.Web.Core.Context;
    using Nootus.Fabric.Web.Security.Common;
    using Nootus.Fabric.Web.Security.Entities;
    using Nootus.Fabric.Web.Security.Identity;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;

    public class ProfileMiddleware
    {
        private RequestDelegate next;

        public ProfileMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context, SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            if (context.User.Identity.IsAuthenticated)
            {
                this.SetNTContext(context);
            }

            // automatically logging in in the dev mode
            else if (SiteSettings.IsEnvironment(SecurityConstants.DevEnvironment))
            {
                ApplicationUser user = await userManager.FindByNameAsync(SecuritySettings.NootusProfileUserName);
                await signInManager.SignInAsync(user, false);
            }

            await this.next(context);
        }

        public void SetNTContext(HttpContext context)
        {
            var claims = context.User.Claims;

            string companyId = context.Request.Headers[SecurityConstants.HeaderCompanyId];
            companyId = companyId ?? claims.Where(c => c.Type == NTClaimTypes.CompanyId).Select(c => c.Value).FirstOrDefault();

            NTContextModel model = new NTContextModel()
            {
                UserId = claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value,
                UserName = context.User.Identity.Name,
                FirstName = claims.First(c => c.Type == NTClaimTypes.FirstName).Value,
                LastName = claims.First(c => c.Type == NTClaimTypes.LastName).Value,
                CompanyId = Convert.ToInt32(companyId ?? "0"),
            };

            // setting the Group CompanyId
            model.GroupCompanyId = PageService.CompanyClaims[model.CompanyId]?.ParentCompanyId ?? model.CompanyId;

            NTContext.Context = model;
        }
    }
}