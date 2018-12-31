using Microsoft.Extensions.DependencyInjection;
using Nootus.Fabric.Web.Core.Context;
using Nootus.Fabric.Web.Core.Models;
using Nootus.Fabric.Web.Security.Core.Middleware;
using Nootus.Fabric.Web.Security.Core.Models;
using Nootus.Fabric.Web.Security.Cosmos.Device;
using Nootus.Fabric.Web.Security.Cosmos.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Nootus.Fabric.Web.Security.Cosmos.Middleware
{
    public static class CacheService
    {
        public static IServiceCollection CachePageClaimsRoles(this IServiceCollection services)
        {
            RepositoryService repository = services.BuildServiceProvider().GetRequiredService<RepositoryService>();
            CachePageClaimsRoles(repository);
            return services;
        }

        public static void CachePageClaimsRoles(RepositoryService repository)
        {
            PageService.Pages = repository.PagesGet().Result;
            PageService.MenuPages = new List<MenuModel>();
            PageService.RoleClaims = repository.RoleClaimsGet().Result;
            PageService.AdminRoles = repository.AdminRolesGet();
        }

        private static string androidSignatureHash;
        public static string AndroidSignatureHash
        {
            get
            {
                if(androidSignatureHash == null)
                {
                    AndroidService service = NTContext.HttpContext.RequestServices.GetRequiredService<AndroidService>();
                    androidSignatureHash = service.SignatureHashGet().Result;
                }

                return androidSignatureHash;
            }

            set
            {
                androidSignatureHash = value;
            }
        }
    }
}
