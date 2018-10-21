//-------------------------------------------------------------------------------------------------
// <copyright file="SecuritySettings.cs" company="Nootus">
//  Copyright (c) Nootus. All rights reserved.
// </copyright>
// <description>
//  Hard coded values for the security project
// </description>
//-------------------------------------------------------------------------------------------------
namespace Nootus.Fabric.Web.Security.Core.Common
{
    using Nootus.Fabric.Web.Security.Core.Models;
    using System.Collections.Generic;

    public static class SecuritySettings
    {
        public const string AnonymousClaim = "Anonymous";
        public const string AnonymouseClaimType = "Home";
        public const string AdminSuffix = "Admin";
        public const string DenySuffix = "_Deny";

        public const string NootusProfileUserName = "PRASANNA@NOOTUS.COM";
        public const int NootusCompanyId = 1;
        public const string DefaultNewUserRole = "RegisteredUser";

        public static List<int> AdminRoles
        {
            get
            {
                return new List<int> { (int)RoleType.SuperAdmin, (int)RoleType.GroupAdmin, (int)RoleType.CompanyAdmin };
            }
        }

        public static List<int> SuperGroupAdminRoles
        {
            get
            {
                return new List<int> { (int)RoleType.SuperAdmin, (int)RoleType.GroupAdmin };
            }
        }
    }
}