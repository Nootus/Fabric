//-------------------------------------------------------------------------------------------------
// <copyright file="ChartEntity.cs" company="Nootus">
//  Copyright (c) Nootus. All rights reserved.
// </copyright>
// <description>
//  Entity Object to store the data returned from database stored procedure
// </description>
//-------------------------------------------------------------------------------------------------
namespace Nootus.Fabric.Web.Core.Models.Dashboard
{
    using System.ComponentModel.DataAnnotations;

    public class ChartEntity<TX, TY>
    {
        [Key]
        public string Id { get; set; }

        public string Key { get; set; }

        public TX X { get; set; }

        public TY Y { get; set; }

        public int KeyOrder { get; set; }

        public int XOrder { get; set; }
    }
}
