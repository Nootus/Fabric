using Microsoft.Extensions.DependencyInjection;
using Nootus.Fabric.Web.Core.Models;
using Nootus.Fabric.Web.Security.Core.Middleware;
using Nootus.Fabric.Web.Security.Core.Models;
using Nootus.Fabric.Web.Security.Cosmos.Repositories;
using System.Collections.Generic;

namespace Nootus.Fabric.Web.Security.Cosmos.Middleware
{
    public static class CacheService
    {
        public static IServiceCollection CachePageClaimsRoles(this IServiceCollection services)
        {
            RepositoryService repository = services.BuildServiceProvider().GetRequiredService<RepositoryService>();
            CacheService.CachePageClaimsRoles(repository);
            return services;
        }

        public static void CachePageClaimsRoles(RepositoryService repository)
        {
            PageService.Pages = repository.PagesGet().Result;
            PageService.MenuPages = new List<MenuModel>();
            PageService.RoleClaims = repository.RoleClaimsGet().Result;
            PageService.AdminRoles = repository.AdminRolesGet();
        }
    }
}
