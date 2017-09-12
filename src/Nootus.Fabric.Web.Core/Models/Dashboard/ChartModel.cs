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

    public class ChartModel<Tx, Ty>
    {
        public string XAxisLabel { get; set; }

        public string YAxisLabel { get; set; }

        public List<ChartDataModel<Tx, Ty>> Data { get; set; }

        public List<Tx> XAxisDataLabels { get; set; }
    }
}
