//-------------------------------------------------------------------------------------------------
// <copyright file="ProfileExtensions.cs" company="Nootus">
//  Copyright (c) Nootus. All rights reserved.
// </copyright>
// <description>
//  Extension methods for user profile functions
// </description>
//-------------------------------------------------------------------------------------------------
namespace Nootus.Fabric.Web.Security.Core.Extensions
{
    using Nootus.Fabric.Web.Security.Core.Common;
    using Nootus.Fabric.Web.Security.Core.Middleware;
    using Nootus.Fabric.Web.Security.Core.Models;
    using System.Collections.Generic;
    using System.Linq;

    public static class ProfileExtensions
    {
        public static void SetMenu(this ProfileModel profile)
        {
            List<PageModel> pages = PageService.Pages;
            List<MenuModel> menuPages = PageService.MenuPages;
            // CompanyEntity companyClaims = PageService.CompanyClaims[profile.CompanyId];
            List<MenuModel> allPages;

            // checking for the module admins
            var rolequery = from menupage in menuPages
                            join page in pages on menupage.PageId equals page.PageId
                            where profile.AdminRoles.Any(r => page.PageClaims.Any(p => r == p.ClaimType + SecuritySettings.AdminSuffix))
                            select menupage;

            var rolePages = rolequery.ToList();

            // checking for permissions per user
            var userquery = from menupage in menuPages
                            join page in pages on menupage.PageId equals page.PageId
                            where page.Claims.Any(p => profile.Claims.Any(c => p.PrimaryClaimInd == true && c.ClaimType == p.Claim.ClaimType && (p.Claim.ClaimValue == SecuritySettings.AnonymousClaim || c.ClaimValue == p.Claim.ClaimValue)))
                            select menupage;

            var userPages = userquery.ToList();

            allPages = rolePages.Union(userPages).ToList();
            /*
            if (profile.CompanyId != SecuritySettings.NootusCompanyId)
            {
                // getting the company permissions
                var qry = from all in allPages
                          join page in pages on all.PageId equals page.PageId
                          where page.Claims.Any(p => companyClaims.Claims.Any(c => c.Claim.ClaimType == p.Claim.ClaimType && (p.Claim.ClaimValue == SecuritySettings.AnonymousClaim || c.Claim.ClaimValue == p.Claim.ClaimValue)))
                          select all;

                allPages = qry.ToList();
            }
            */

            // converting this into tree
            List<MenuModel> treeMenu = allPages.Where(m => m.ParentPageId == null).OrderBy(o => o.DisplayOrder).ToList();

            foreach (var tree in treeMenu)
            {
                tree.Items = allPages.Where(m => m.ParentPageId == tree.PageId).OrderBy(o => o.DisplayOrder).ToList();

                // checking whether the last one is group menu and removing it
                if (tree.Items[tree.Items.Count - 1].GroupMenuInd)
                {
                    tree.Items.RemoveAt(tree.Items.Count - 1);
                }
            }

            profile.Menu = treeMenu;
        }
    }
}