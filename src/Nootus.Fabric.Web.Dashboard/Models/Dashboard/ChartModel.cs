//-------------------------------------------------------------------------------------------------
// <copyright file="ChartModel.cs" company="Nootus">
//  Copyright (c) Nootus. All rights reserved.
// </copyright>
// <description>
//  DTO to carry chart data to UI
// </description>
//-------------------------------------------------------------------------------------------------
namespace Nootus.Fabric.Web.Core.Models.Dashboard
{
    using System.Collections.Generic;

    public class ChartModel<TX, TY>
    {
        public string XAxisLabel { get; set; }

        public string YAxisLabel { get; set; }

        public List<ChartDataModel<TX, TY>> Data { get; set; }

        public List<TX> XAxisDataLabels { get; set; }
    }
}
