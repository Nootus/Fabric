//-------------------------------------------------------------------------------------------------
// <copyright file="MenuPageEntity.cs" company="Nootus">
//  Copyright (c) Nootus. All rights reserved.
// </copyright>
// <description>
//  Menu to be shown in the UI
// </description>
//-------------------------------------------------------------------------------------------------
namespace Nootus.Fabric.Web.Security.SqlServer.Entities
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("MenuPage", Schema = "security")]
    public class MenuPageEntity
    {
        [Key]
        public int MenuPageId { get; set; }

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