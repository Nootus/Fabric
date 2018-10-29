//-------------------------------------------------------------------------------------------------
// <copyright file="RoleModel.cs" company="Nootus">
//  Copyright (c) Nootus. All rights reserved.
// </copyright>
// <description>
//  Contains role info
// </description>
//-------------------------------------------------------------------------------------------------
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Nootus.Fabric.Web.Security.Core.Models
{
    public class RoleModel
    {
        [JsonIgnore]
        public string Id { get; set; }
        public string Name { get; set; }
        public int CompanyId { get; set; }
        public int RoleType { get; set; }
        public List<ClaimModel> Claims { get; set; }
        public List<string> RoleHierarchy { get; set; }
    }
}