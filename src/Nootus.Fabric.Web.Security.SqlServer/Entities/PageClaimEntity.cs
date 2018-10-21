//-------------------------------------------------------------------------------------------------
// <copyright file="PageClaimEntity.cs" company="Nootus">
//  Copyright (c) Nootus. All rights reserved.
// </copyright>
// <description>
//  Permissions for each page
// </description>
//-------------------------------------------------------------------------------------------------
namespace Nootus.Fabric.Web.Security.SqlServer.Entities
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("PageClaim", Schema = "security")]
    public class PageClaimEntity
    {
        [Key]
        public int Id { get; set; }

        public int PageId { get; set; }

        public int ClaimId { get; set; }

        public bool PrimaryClaimInd { get; set; }

        [ForeignKey("ClaimId")]
        public ClaimEntity Claim { get; set; }
    }
}