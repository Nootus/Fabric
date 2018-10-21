//-------------------------------------------------------------------------------------------------
// <copyright file="ChartModel.cs" company="Nootus">
//  Copyright (c) Nootus. All rights reserved.
// </copyright>
// <description>
//  Used in Angular UI
// </description>
//-------------------------------------------------------------------------------------------------
namespace Nootus.Fabric.Web.Core.Models.Widget
{
    public class ChartModel
    {
        public int TypeId { get; set; }

        public string Type { get; set; }

        public object Model { get; set; }
    }
}
