//-------------------------------------------------------------------------------------------------
// <copyright file="RoleModel.cs" company="Nootus">
//  Copyright (c) Nootus. All rights reserved.
// </copyright>
// <description>
//  Contains role info
// </description>
//-------------------------------------------------------------------------------------------------
namespace Nootus.Fabric.Web.Security.Models
{
    public class RoleModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public int CompanyId { get; set; }

        public int RoleType { get; set; }
    }
}