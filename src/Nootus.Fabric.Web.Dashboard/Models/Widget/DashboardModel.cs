//-------------------------------------------------------------------------------------------------
// <copyright file="DashboardModel.cs" company="Nootus">
//  Copyright (c) Nootus. All rights reserved.
// </copyright>
// <description>
//  DTO to send the widget details to a dashboard in Angular
// </description>
//-------------------------------------------------------------------------------------------------
namespace Nootus.Fabric.Web.Core.Models.Widget
{
    using System.Collections.Generic;

    public class DashboardModel
    {
        public List<WidgetModel> AllWidgets { get; set; }

        public List<PageWidgetModel> PageWidgets { get; set; }
    }
}
