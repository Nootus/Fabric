//-------------------------------------------------------------------------------------------------
// <copyright file="RoleHierarchyEntity.cs" company="Nootus">
//  Copyright (c) Nootus. All rights reserved.
// </copyright>
// <description>
//  Role hierarchy of the roles. Upper level roles will have permissions of lower level
// </description>
//-------------------------------------------------------------------------------------------------
namespace Nootus.Fabric.Web.Security.Entities
{
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("RoleHierarchy", Schema = "security")]
    public class RoleHierarchyEntity
    {
        public string RoleId { get; set; }

        public string ChildRoleId { get; set; }
    }
}