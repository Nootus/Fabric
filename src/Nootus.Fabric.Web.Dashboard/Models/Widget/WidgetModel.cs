//-------------------------------------------------------------------------------------------------
// <copyright file="WidgetModel.cs" company="Nootus">
//  Copyright (c) Nootus. All rights reserved.
// </copyright>
// <description>
//  Contains the data necessary to represent a widget/chart in UI
// </description>
//-------------------------------------------------------------------------------------------------
namespace Nootus.Fabric.Web.Core.Models.Widget
{
    public class WidgetModel
    {
        public int WidgetId { get; set; }

        public string Name { get; set; }

        public string Claim { get; set; }

        public int SizeX { get; set; }

        public int SizeY { get; set; }

        public string XAxisLabel { get; set; }

        public string YAxisLabel { get; set; }

        public ChartModel Chart { get; set; }
    }
}
