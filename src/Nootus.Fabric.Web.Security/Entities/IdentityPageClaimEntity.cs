//-------------------------------------------------------------------------------------------------
// <copyright file="IdentityPageClaimEntity.cs" company="Nootus">
//  Copyright (c) Nootus. All rights reserved.
// </copyright>
// <description>
//  Permissions for each page
// </description>
//-------------------------------------------------------------------------------------------------
namespace Nootus.Fabric.Web.Security.Entities
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("IdentityPageClaim", Schema = "security")]
    public class IdentityPageClaimEntity
    {
        [Key]
        public int Id { get; set; }

        public int PageId { get; set; }

        public int ClaimId { get; set; }

        public bool PrimaryClaimInd { get; set; }

        [ForeignKey("ClaimId")]
        public IdentityClaimEntity Claim { get; set; }
    }
}