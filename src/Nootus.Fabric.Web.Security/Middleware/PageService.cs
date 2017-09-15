//-------------------------------------------------------------------------------------------------
// <copyright file="PageService.cs" company="Nootus">
//  Copyright (c) Nootus. All rights reserved.
// </copyright>
// <description>
//  Service used to cache the data necessary for each page
// </description>
//-------------------------------------------------------------------------------------------------
namespace Nootus.Fabric.Web.Security.Middleware
{
    using System.Collections.Generic;
    using Nootus.Fabric.Web.Core.Models;
    using Nootus.Fabric.Web.Security.Entities;
    using Nootus.Fabric.Web.Security.Models;
    using Nootus.Fabric.Web.Security.Repositories;

    public class PageService
    {
        public static List<PageModel> Pages { get; set; }

        public static List<MenuModel> MenuPages { get; set; }

        public static List<ListItem<string, string>> AdminRoles { get; set; }

        public static Dictionary<int, CompanyEntity> CompanyClaims { get; set; }

        public static void CachePageClaimsRoles(SecurityRepository repository)
        {
            Pages = repository.PagesGet();
            MenuPages = repository.MenuPagesGet();
            AdminRoles = repository.AdminRolesGet();

            CacheCompanyClaims(repository);
        }

        private static void CacheCompanyClaims(SecurityRepository repository)
        {
            var companies = repository.CompanyClaimsGet();
            CompanyClaims = new Dictionary<int, CompanyEntity>();
            foreach (var company in companies)
            {
                CompanyClaims.Add(company.CompanyId, company);
            }
        }
    }
}
