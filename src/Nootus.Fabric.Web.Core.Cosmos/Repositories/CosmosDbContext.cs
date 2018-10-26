//-------------------------------------------------------------------------------------------------
// <copyright file="CosmosDbMiddlewareExtensions.cs" company="Nootus">
//  Copyright (c) Nootus. All rights reserved.
// </copyright>
// <description>
//  Extension method to invoke Cosmos Middleware modules
// </description>
//-------------------------------------------------------------------------------------------------
namespace Nootus.Fabric.Web.Core.Cosmos.Repositories
{
    using Microsoft.Azure.Documents.Client;
    using Nootus.Fabric.Web.Core.Cosmos.Models;
    using System;

    public abstract class CosmosDbContext
    {
        public CosmosDbContext()
        {
            Client = new DocumentClient(new Uri(Settings.Endpoint), Settings.Key,
                new ConnectionPolicy
                {
                    ConnectionMode = ConnectionMode.Direct,
                    ConnectionProtocol = Protocol.Tcp
                });
        }

        public DocumentClient Client { get; set; }

        public abstract DatabaseSettings Settings { get; set; }
    }
}
