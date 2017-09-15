//-------------------------------------------------------------------------------------------------
// <copyright file="CompanyClaimEntity.cs" company="Nootus">
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

    [Table("CompanyClaim", Schema = "security")]
    public class CompanyClaimEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CompanyClaimId { get; set; }

        public int CompanyId { get; set; }

        public int ClaimId { get; set; }

        [ForeignKey("ClaimId")]
        public ClaimEntity Claim { get; set; }
    }
}