//-------------------------------------------------------------------------------------------------
// <copyright file="RoleType.cs" company="Nootus">
//  Copyright (c) Nootus. All rights reserved.
// </copyright>
// <description>
//  Enum to store the super users in the system
// </description>
//-------------------------------------------------------------------------------------------------
namespace Nootus.Fabric.Web.Security.Core.Models
{
    public enum RoleType
    {
        SuperAdmin = 1,
        GroupAdmin = 2,
        CompanyAdmin = 3,
        ModuleAdmin = 4,
        UserDefined = 5,
    }
}