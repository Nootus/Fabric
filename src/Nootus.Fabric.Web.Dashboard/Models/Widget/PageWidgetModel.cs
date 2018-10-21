//-------------------------------------------------------------------------------------------------
// <copyright file="PageWidgetModel.cs" company="Nootus">
//  Copyright (c) Nootus. All rights reserved.
// </copyright>
// <description>
//  Contains the details of each widget in a dashboard page
// </description>
//-------------------------------------------------------------------------------------------------
namespace Nootus.Fabric.Web.Core.Models.Widget
{
    public class PageWidgetModel
    {
        public int DashboardPageWidgetId { get; set; }

        public int WidgetId { get; set; }

        public WidgetOptionsModel WidgetOptions { get; set; }
    }
}
