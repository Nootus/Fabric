//-------------------------------------------------------------------------------------------------
// <copyright file="PageService.cs" company="Nootus">
//  Copyright (c) Nootus. All rights reserved.
// </copyright>
// <description>
//  Service used to cache the data necessary for each page
// </description>
//-------------------------------------------------------------------------------------------------
namespace Nootus.Fabric.Web.Security.Core.Middleware
{
    using Nootus.Fabric.Web.Core.Models;
    using Nootus.Fabric.Web.Security.Core.Models;
    using System.Collections.Generic;

    public static class PageService
    {
        public static List<PageModel> Pages { get; set; }

        public static List<MenuModel> MenuPages { get; set; }

        public static List<ListItem<string, string>> AdminRoles { get; set; }

        //public static Dictionary<int, CompanyEntity> CompanyClaims { get; set; }
    }
}
