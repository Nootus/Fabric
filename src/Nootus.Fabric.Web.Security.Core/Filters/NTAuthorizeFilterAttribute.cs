//-------------------------------------------------------------------------------------------------
// <copyright file="NTAuthorizeFilterAttribute.cs" company="Nootus">
//  Copyright (c) Nootus. All rights reserved.
// </copyright>
// <description>
//  MVC filter to authorize user for a page using claims
// </description>
//-------------------------------------------------------------------------------------------------
namespace Nootus.Fabric.Web.Security.Core.Filters
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Authorization;
    using Microsoft.AspNetCore.Mvc.Filters;
    using Nootus.Fabric.Web.Core.Context;
    using Nootus.Fabric.Web.Security.Core.Common;
    using Nootus.Fabric.Web.Security.Core.Identity;
    using Nootus.Fabric.Web.Security.Core.Middleware;
    using System;
    using System.Linq;
    using System.Security.Claims;

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class NTAuthorizeFilterAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            // checking for AllowAnonymous
            if (context.Filters.Any(f => f is AllowAnonymousFilter))
            {
                return;
            }

            // getting the current module and claim
            string action = context.RouteData.Values["action"].ToString().ToLower();
            string controller = context.RouteData.Values["controller"].ToString().ToLower() + "controller";

            var page = PageService.Pages.Where(c => string.Compare(c.Controller, controller, true) == 0 && string.Compare(c.ActionMethod, action, true) == 0).FirstOrDefault();

            if (page == null)
            {
                context.Result = new StatusCodeResult(403);
                return;
            }

            // setting the current DashbaordInd
            if (page.DashboardInd)
            {
                NTContext.Context.DashboardPageId = page.PageId;
            }

            // checking for annonymous claim
            if (page.PageClaims.Any(p => p.ClaimType == SecuritySettings.AnonymouseClaimType && p.ClaimValue == SecuritySettings.AnonymousClaim))
            {
                return;
            }

            var userClaims = context.HttpContext.User.Claims;

            // checking the companyid passed in headers
            string companies = userClaims.Where(c => c.Type == NTClaimTypes.Companies).Select(c => c.Value).FirstOrDefault();
            string companyId;

            if (context.HttpContext.Request.Headers.ContainsKey(SecurityConstants.HeaderCompanyId))
            {
                companyId = context.HttpContext.Request.Headers[SecurityConstants.HeaderCompanyId];
            }
            else
            {
                companyId = userClaims.Where(c => c.Type == NTClaimTypes.CompanyId).Select(c => c.Value).FirstOrDefault();
            }

            if (companies == null || companyId == null || !companies.Split(',').Contains(companyId))
            {
                context.Result = new StatusCodeResult(403);
                return;
            }

            // checking for annonymous claim for each module
            if (page.PageClaims.Any(p => p.ClaimValue == SecuritySettings.AnonymousClaim))
            {
                return;
            }

            // getting current roles and then get all the child roles
            string[] roles = userClaims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value).ToArray();
            roles = PageService.AdminRoles.Where(r => roles.Contains(r.Key)).Select(r => r.Item).ToArray();

            // checking whether user is an admin
            if (!roles.Any(r => page.PageClaims.Any(p => r == p.ClaimType + SecuritySettings.AdminSuffix)))
            {
                // checking for deny claim
                if (userClaims.Any(c => page.PageClaims.Any(p => c.Type == p.ClaimType + SecuritySettings.DenySuffix && c.Value == p.ClaimValue)))
                {
                    context.Result = new StatusCodeResult(403);  // new HttpUnauthorizedResult();
                }

                // checking for current claim
                else if (!userClaims.Any(c => page.PageClaims.Any(p => c.Type == p.ClaimType && c.Value == p.ClaimValue)))
                {
                    context.Result = new StatusCodeResult(403);
                }
            }
        }
    }
}