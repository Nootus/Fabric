//-------------------------------------------------------------------------------------------------
// <copyright file="WidgetOptionsModel.cs" company="Nootus">
//  Copyright (c) Nootus. All rights reserved.
// </copyright>
// <description>
//  The length, width and position for each widget on the page
// </description>
//-------------------------------------------------------------------------------------------------
namespace Nootus.Fabric.Web.Core.Models.Widget
{
    public class WidgetOptionsModel
    {
        public int Columns { get; set; }

        public int Rows { get; set; }

        public int SizeX { get; set; }

        public int SizeY { get; set; }
    }
}
