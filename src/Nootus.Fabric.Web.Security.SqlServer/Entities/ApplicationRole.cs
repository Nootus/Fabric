//-------------------------------------------------------------------------------------------------
// <copyright file="ApplicationRole.cs" company="Nootus">
//  Copyright (c) Nootus. All rights reserved.
// </copyright>
// <description>
//  Customization of IdentityRole to add Company and RoleType
// </description>
//-------------------------------------------------------------------------------------------------
namespace Nootus.Fabric.Web.Security.SqlServer.Entities
{
    using Microsoft.AspNetCore.Identity;

    public class ApplicationRole : IdentityRole
    {
        public int CompanyId { get; set; }

        public int RoleType { get; set; }
    }
}