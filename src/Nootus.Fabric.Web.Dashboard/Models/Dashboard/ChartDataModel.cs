//-------------------------------------------------------------------------------------------------
// <copyright file="ChartDataModel.cs" company="Nootus">
//  Copyright (c) Nootus. All rights reserved.
// </copyright>
// <description>
//  DTO to hold Chart data
// </description>
//-------------------------------------------------------------------------------------------------
namespace Nootus.Fabric.Web.Core.Models.Dashboard
{
    using System.Collections.Generic;

    public class ChartDataModel<TX, TY>
    {
        public string Key { get; set; }

        public int Order { get; set; }

        public List<ChartPointModel<TX, TY>> Values { get; set; }
    }
}
