//-------------------------------------------------------------------------------------------------
// <copyright file="IdentityCompanyClaimEntity.cs" company="Nootus">
//  Copyright (c) Nootus. All rights reserved.
// </copyright>
// <description>
//  Claims for each company
// </description>
//-------------------------------------------------------------------------------------------------
namespace Nootus.Fabric.Web.Security.Entities
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("IdentityCompanyClaim", Schema = "security")]
    public class IdentityCompanyClaimEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdentityCompanyClaimId { get; set; }

        public int CompanyId { get; set; }

        public int ClaimId { get; set; }

        [ForeignKey("ClaimId")]
        public IdentityClaimEntity Claim { get; set; }
    }
}