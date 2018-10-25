//-------------------------------------------------------------------------------------------------
// <copyright file="MenuModel.cs" company="Nootus">
//  Copyright (c) Nootus. All rights reserved.
// </copyright>
// <description>
//  Used to store hierarcial menu
// </description>
//-------------------------------------------------------------------------------------------------
namespace Nootus.Fabric.Web.Security.Core.Models
{
    using System.Collections.Generic;

    public class MenuModel
    {
        public int PageId { get; set; }
        public string MenuText { get; set; }
        public int? ParentPageId { get; set; }
        public bool GroupMenuInd { get; set; }
        public int DisplayOrder { get; set; }
        public string Url { get; set; }
        public string IconCss { get; set; }
        public List<MenuModel> Items { get; set; }
    }
}