//-------------------------------------------------------------------------------------------------
// <copyright file="CompanyEntity.cs" company="Nootus">
//  Copyright (c) Nootus. All rights reserved.
// </copyright>
// <description>
//  Contains the company details along with it's claims
// </description>
//-------------------------------------------------------------------------------------------------
namespace Nootus.Fabric.Web.Security.SqlServer.Entities
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Nootus.Fabric.Web.Core.SqlServer.Entities;

    [Table("Company", Schema = "security")]
    public class CompanyEntity : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public new int CompanyId
        {
            get
            {
                return base.CompanyId;
            }

            set
            {
                base.CompanyId = value;
            }
        }

        public string CompanyName { get; set; }

        public bool GroupInd { get; set; }

        public int? ParentCompanyId { get; set; }

        public List<CompanyClaimEntity> Claims { get; set; }
    }
}