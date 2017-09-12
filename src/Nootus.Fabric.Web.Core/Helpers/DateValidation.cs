//-------------------------------------------------------------------------------------------------
// <copyright file="DateValidation.cs" company="Nootus">
//  Copyright (c) Nootus. All rights reserved.
// </copyright>
// <description>
//  To validate startTime less than endTime. Also to validate in a list without overlapping
// </description>
//-------------------------------------------------------------------------------------------------
namespace Nootus.Fabric.Web.Core.Helpers
{
    using System.Collections.Generic;
    using Nootus.Fabric.Web.Core.Models;

    public static class DateValidation
    {
        public static bool ValidateTimeRange(this ITimeRange range)
        {
            return range.StartTime < range.EndTime;
        }

        public static bool ValidateTimeRange<T>(this List<T> ranges)
            where T : ITimeRange
        {
            for (int counter = 0; counter < ranges.Count; counter++)
            {
                ITimeRange range = ranges[counter];
                if (!range.ValidateTimeRange())
                {
                    return false;
                }

                for (int subCounter = counter + 1; subCounter < ranges.Count; subCounter++)
                {
                    ITimeRange subRange = ranges[subCounter];

                    if ((subRange.StartTime <= range.StartTime & range.StartTime <= subRange.EndTime) || (subRange.StartTime < range.EndTime & range.EndTime <= subRange.EndTime) || (range.StartTime <= subRange.StartTime && range.EndTime >= subRange.EndTime))
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}
