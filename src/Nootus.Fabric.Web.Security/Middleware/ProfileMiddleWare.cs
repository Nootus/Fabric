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
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Nootus.Fabric.Web.Core.Common;
    using Nootus.Fabric.Web.Core.Context;
    using Nootus.Fabric.Web.Security.Common;
    using Nootus.Fabric.Web.Security.Entities;
    using Nootus.Fabric.Web.Security.Identity;

    public class ProfileMiddleware
    {
        private RequestDelegate next;
        private SignInManager<ApplicationUser> signInManager;
        private UserManager<ApplicationUser> userManager;

        public ProfileMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context, SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            // setting local variables
            this.signInManager = signInManager;
            this.userManager = userManager;

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
            model.GroupCompanyId = PageService.CompanyClaims[model.CompanyId]?.ParentCompanyId ?? model.CompanyId;

            NTContext.Context = model;
        }

        private async Task LoginDevEnvironment()
        {
            if (SiteSettings.IsEnvironment(SecurityConstants.DevEnvironment))
            {
                ApplicationUser user = await this.userManager.FindByNameAsync(SecuritySettings.NootusProfileUserName);
                await this.signInManager.SignInAsync(user, false);
            }
        }
    }
}