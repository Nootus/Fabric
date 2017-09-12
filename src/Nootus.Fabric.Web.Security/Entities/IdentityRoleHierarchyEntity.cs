//-------------------------------------------------------------------------------------------------
// <copyright file="IdentityRoleHierarchyEntity.cs" company="Nootus">
//  Copyright (c) Nootus. All rights reserved.
// </copyright>
// <description>
//  Role hierarchy of the roles. Upper level roles will have permissions of lower level
// </description>
//-------------------------------------------------------------------------------------------------
namespace Nootus.Fabric.Web.Security.Entities
{
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("IdentityRoleHierarchy", Schema = "security")]
    public class IdentityRoleHierarchyEntity
    {
        public string RoleId { get; set; }

        public string ChildRoleId { get; set; }
    }
}