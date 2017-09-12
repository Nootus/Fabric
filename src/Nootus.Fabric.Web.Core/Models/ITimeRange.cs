//-------------------------------------------------------------------------------------------------
// <copyright file="ITimeRange.cs" company="Nootus">
//  Copyright (c) Nootus. All rights reserved.
// </copyright>
// <description>
//  Used in validating StartTime and EndTime
// </description>
//-------------------------------------------------------------------------------------------------
namespace Nootus.Fabric.Web.Core.Models
{
    using System;

    public interface ITimeRange
    {
        DateTime StartTime { get; set; }

        DateTime EndTime { get; set; }
    }
}
