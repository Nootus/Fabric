//-------------------------------------------------------------------------------------------------
// <copyright file="ChartPointModel.cs" company="Nootus">
//  Copyright (c) Nootus. All rights reserved.
// </copyright>
// <description>
//  For each unit on the chart, storing x, y and order
// </description>
//-------------------------------------------------------------------------------------------------
namespace Nootus.Fabric.Web.Core.Models.Dashboard
{
    public class ChartPointModel<TX, TY>
    {
        public TX X { get; set; }

        public TY Y { get; set; }

        public int Order { get; set; }
    }
}
