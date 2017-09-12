//-------------------------------------------------------------------------------------------------
// <copyright file="SecuritySettings.cs" company="Nootus">
//  Copyright (c) Nootus. All rights reserved.
// </copyright>
// <description>
//  Hard coded values for the security project
// </description>
//-------------------------------------------------------------------------------------------------
namespace Nootus.Fabric.Web.Security.Common
{
    using Nootus.Fabric.Web.Security.Models;

    public static class SecuritySettings
    {
        public const string AnonymousClaim = "Anonymous";
        public const string AnonymouseClaimType = "Home";
        public const string AdminSuffix = "Admin";
        public const string DenySuffix = "_Deny";

        public const string NootusProfileUserName = "PRASANNA@NOOTUS.COM";
        public const int NootusCompanyId = 1;

        public static int[] AdminRoles
        {
            get
            {
                return new int[] { (int)RoleType.SuperAdmin, (int)RoleType.GroupAdmin, (int)RoleType.CompanyAdmin };
            }
        }

        public static int[] SuperGroupAdminRoles
        {
            get
            {
                return new int[] { (int)RoleType.SuperAdmin, (int)RoleType.GroupAdmin };
            }
        }
    }
}