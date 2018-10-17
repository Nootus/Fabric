//-------------------------------------------------------------------------------------------------
// <copyright file="BaseDashboardDomain.cs" company="Nootus">
//  Copyright (c) Nootus. All rights reserved.
// </copyright>
// <description>
//  Abstract class for Dashboards widgets
// </description>
//-------------------------------------------------------------------------------------------------
namespace Nootus.Fabric.Web.Core.Models.Dashboard
{
    using System.Threading.Tasks;
    using Nootus.Fabric.Web.Core.Context;

    public abstract class BaseDashboardDomain
    {
        public abstract Task<ChartModel<string, int>> GetWidgetData(int widgetId, WidgetOptions options);
    }
}
