//-------------------------------------------------------------------------------------------------
// <copyright file="UserCompanyEntity.cs" company="Nootus">
//  Copyright (c) Nootus. All rights reserved.
// </copyright>
// <description>
//  Companies the logged in user has permissioned to
// </description>
//-------------------------------------------------------------------------------------------------
namespace Nootus.Fabric.Web.Security.Entities
{
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("UserCompany", Schema = "security")]
    public class UserCompanyEntity
    {
        public string UserProfileId { get; set; }

        public int CompanyId { get; set; }
    }
}