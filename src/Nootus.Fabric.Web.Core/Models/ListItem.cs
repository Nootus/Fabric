//-------------------------------------------------------------------------------------------------
// <copyright file="ListItem.cs" company="Nootus">
//  Copyright (c) Nootus. All rights reserved.
// </copyright>
// <description>
//  Generic Key Value pair to transfer data to UI
// </description>
//-------------------------------------------------------------------------------------------------
namespace Nootus.Fabric.Web.Core.Models
{
    public class ListItem<TKey, TItem>
    {
        public TKey Key { get; set; }

        public TItem Item { get; set; }
    }
}
