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

    public class ChartEntity<Tx, Ty>
    {
        [Key]
        public string Id { get; set; }

        public string Key { get; set; }

        public Tx X { get; set; }

        public Ty Y { get; set; }

        public int KeyOrder { get; set; }

        public int XOrder { get; set; }
    }
}
