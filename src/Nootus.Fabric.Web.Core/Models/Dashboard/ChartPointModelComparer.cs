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

    public class ChartPointModelComparer<Tx, Ty> : IEqualityComparer<ChartPointModel<Tx, Ty>>
    {
        public bool Equals(ChartPointModel<Tx, Ty> x, ChartPointModel<Tx, Ty> y)
        {
            return x.X.Equals(y.X);
        }

        public int GetHashCode(ChartPointModel<Tx, Ty> obj)
        {
            return obj.X.GetHashCode();
        }
    }
}
