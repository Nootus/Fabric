//-------------------------------------------------------------------------------------------------
// <copyright file="CacheService.cs" company="Nootus">
//  Copyright (c) Nootus. All rights reserved.
// </copyright>
// <description>
//  Caches page claims
// </description>
//-------------------------------------------------------------------------------------------------
namespace Nootus.Fabric.Web.Security.SqlServer.Middleware
{
    using Microsoft.Extensions.DependencyInjection;
    using Nootus.Fabric.Web.Security.Core.Middleware;
    using Nootus.Fabric.Web.Security.SqlServer.Repositories;

    public static class CacheService
    {
        public static IServiceCollection CachePageClaimsRoles(this IServiceCollection services)
        {
            SecurityRepository repository = services.BuildServiceProvider().GetRequiredService<SecurityRepository>();
            CacheService.CachePageClaimsRoles(repository);
            return services;
        }

        private static void CachePageClaimsRoles(SecurityRepository repository)
        {
            PageService.Pages = repository.PagesGet();
            PageService.MenuPages = repository.MenuPagesGet();
            PageService.AdminRoles = repository.AdminRolesGet();

            //CacheCompanyClaims(repository);
        }
        /*
        private static void CacheCompanyClaims(SecurityRepository repository)
        {
            var companies = repository.CompanyClaimsGet();
            CompanyClaims = new Dictionary<int, CompanyEntity>();
            foreach (var company in companies)
            {
                CompanyClaims.Add(company.CompanyId, company);
            }
        }
        */

    }
}
