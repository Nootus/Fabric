//-------------------------------------------------------------------------------------------------
// <copyright file="PageModel.cs" company="Nootus">
//  Copyright (c) Nootus. All rights reserved.
// </copyright>
// <description>
//  Model class to store the details and passed to UI
// </description>
//-------------------------------------------------------------------------------------------------
namespace Nootus.Fabric.Web.Security.Core.Models
{
    using System.Collections.Generic;

    public class PageModel
    {
        public int PageId { get; set; }
        public string Text { get; set; }
        public string Controller { get; set; }
        public string ActionMethod { get; set; }
        public bool DashboardInd { get; set; }
        public ClaimModel PrimaryClaim { get; set; }
        public List<ClaimModel> Claims { get; set; }
    }
}
