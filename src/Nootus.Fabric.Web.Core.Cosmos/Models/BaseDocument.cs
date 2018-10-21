//-------------------------------------------------------------------------------------------------
// <copyright file="BaseDocument.cs" company="Nootus">
//  Copyright (c) Nootus. All rights reserved.
// </copyright>
// <description>
//  This is the base class for every document in Cosmos
// </description>
//-------------------------------------------------------------------------------------------------
namespace Nootus.Fabric.Web.Core.Cosmos.Models
{
    public abstract class BaseDocument
    {
        public abstract string Type { get; }
    }
}
