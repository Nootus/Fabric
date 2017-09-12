//-------------------------------------------------------------------------------------------------
// <copyright file="IdentityMenuPageEntity.cs" company="Nootus">
//  Copyright (c) Nootus. All rights reserved.
// </copyright>
// <description>
//  Menu to be shown in the UI
// </description>
//-------------------------------------------------------------------------------------------------
namespace Nootus.Fabric.Web.Security.Entities
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("IdentityMenuPage", Schema = "security")]
    public class IdentityMenuPageEntity
    {
        [Key]
        public int IdentityMenuPageId { get; set; }

        public int PageId { get; set; }

        public string MenuText { get; set; }

        public int? ParentPageId { get; set; }

        public bool GroupMenuInd { get; set; }

        public int DisplayOrder { get; set; }

        public string Url { get; set; }

        public string IconCss { get; set; }

        public bool DeletedInd { get; set; }
    }
}