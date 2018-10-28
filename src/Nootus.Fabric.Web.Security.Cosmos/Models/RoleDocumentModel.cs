using Nootus.Fabric.Web.Security.Core.Models;
using System.Collections.Generic;

namespace Nootus.Fabric.Web.Security.Cosmos.Models
{
    public class RoleDocumentModel: RoleModel
    {
        public List<ClaimModel> Claims { get; set; }
    }
}
