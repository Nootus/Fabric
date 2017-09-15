//-------------------------------------------------------------------------------------------------
// <copyright file="PageEntity.cs" company="Nootus">
//  Copyright (c) Nootus. All rights reserved.
// </copyright>
// <description>
//  Contains all the pages in the application
// </description>
//-------------------------------------------------------------------------------------------------
namespace Nootus.Fabric.Web.Security.Entities
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    [Table("Page", Schema = "security")]
    public class PageEntity
    {
        [Key]
        public int PageId { get; set; }

        public string Text { get; set; }

        public string Controller { get; set; }

        public string ActionMethod { get; set; }

        public bool DashboardInd { get; set; }

        public List<PageClaimEntity> Claims { get; set; }
    }
}