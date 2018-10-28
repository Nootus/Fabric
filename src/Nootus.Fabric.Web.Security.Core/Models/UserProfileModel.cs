//-------------------------------------------------------------------------------------------------
// <copyright file="ProfileModel.cs" company="Nootus">
//  Copyright (c) Nootus. All rights reserved.
// </copyright>
// <description>
//  Contains user permission details
// </description>
//-------------------------------------------------------------------------------------------------
namespace Nootus.Fabric.Web.Security.Core.Models
{
    using System.Collections.Generic;

    public class UserProfileModel
    {
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string FullName
        {
            get
            {
                return this.FirstName + " " + this.LastName;
            }
        }

        public int CompanyId { get; set; }
        public List<string> AdminRoles { get; set; }
        public List<RoleModel> Roles { get; set; } = new List<RoleModel>();
        public List<ClaimModel> Claims { get; set; }
        public List<CompanyModel> Companies { get; set; }
        public List<MenuModel> Menu { get; set; }
    }
}
