//-------------------------------------------------------------------------------------------------
// <copyright file="ChartPointModelComparer.cs" company="Nootus">
//  Copyright (c) Nootus. All rights reserved.
// </copyright>
// <description>
//  Used to compare two chart points are same. This is used in sorting by linq query
// </description>
//-------------------------------------------------------------------------------------------------
namespace Nootus.Fabric.Web.Core.Models.Dashboard
{
    using System.Collections.Generic;

    public class ChartPointModelComparer<TX, TY> : IEqualityComparer<ChartPointModel<TX, TY>>
    {
        public bool Equals(ChartPointModel<TX, TY> x, ChartPointModel<TX, TY> y)
        {
            return x.X.Equals(y.X);
        }

        public int GetHashCode(ChartPointModel<TX, TY> obj)
        {
            return obj.X.GetHashCode();
        }
    }
}
