using Nootus.Fabric.Web.Core.Cosmos.Models;
using Nootus.Fabric.Web.Core.Cosmos.Repositories;
using Nootus.Fabric.Web.Core.Models;
using Nootus.Fabric.Web.Security.Core.Middleware;
using Nootus.Fabric.Web.Security.Core.Models;
using Nootus.Fabric.Web.Security.Cosmos.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nootus.Fabric.Web.Security.Cosmos.Repositories
{
    public class RepositoryService : BaseService<SecurityDbContext>
    {
        public RepositoryService(CosmosDbService<SecurityDbContext> dbService) : base(dbService)
        {
        }

        public async Task<List<PageModel>> PagesGet()
        {
            string pageDocumentType = SecurityAppSettings.ServiceSettings.DocumentTypes.Page;
            return await DbService.GetModelByKeyAsyc<List<PageModel>>(pageDocumentType, pageDocumentType);
        }

        public async Task<List<RoleModel>> RoleClaimsGet()
        {
            string roleDocumentType = SecurityAppSettings.ServiceSettings.DocumentTypes.Role;
            return await DbService.GetModelByKeyAsyc<List<RoleModel>>(roleDocumentType, roleDocumentType);
        }

        public List<ListItem<string, string>> AdminRolesGet()
        {
            List<RoleModel> roles = PageService.RoleClaims;

            List<RoleModel> hierarchyRoles = roles.Where(r => r.RoleHierarchy != null).ToList();

            List<ListItem<string, string>> adminRoles = new List<ListItem<string, string>>();
            foreach(RoleModel role in hierarchyRoles)
            {
                foreach(string childRole in role.RoleHierarchy)
                {
                    adminRoles.Add(new ListItem<string, string>() { Key = role.Name, Item = childRole });
                }
            }

            return adminRoles;
        }
    }
}
