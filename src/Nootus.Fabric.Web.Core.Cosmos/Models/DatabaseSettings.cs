//-------------------------------------------------------------------------------------------------
// <copyright file="DatabaseSettings.cs" company="Nootus">
//  Copyright (c) Nootus. All rights reserved.
// </copyright>
// <description>
//  Cosmos DB Settings
// </description>
//-------------------------------------------------------------------------------------------------
namespace Nootus.Fabric.Web.Core.Cosmos.Models
{
    public class DatabaseSettings
    {
        public string Endpoint { get; set; }
        public string Key { get; set; }
        public string DatabaseId { get; set; }
        public string CollectionId { get; set; }
    }
}
