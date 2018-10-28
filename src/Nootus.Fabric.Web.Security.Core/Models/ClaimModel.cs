//-------------------------------------------------------------------------------------------------
// <copyright file="ClaimModel.cs" company="Nootus">
//  Copyright (c) Nootus. All rights reserved.
// </copyright>
// <description>
//  Storing the Claims
// </description>
//-------------------------------------------------------------------------------------------------
using Newtonsoft.Json;

namespace Nootus.Fabric.Web.Security.Core.Models
{
    public class ClaimModel
    {
        [JsonIgnore]
        public int Id { get; set; }

        [JsonIgnore]
        public string ClaimType { get; set; }

        [JsonIgnore]
        public string ClaimValue { get; set; }

        public string Claim {
            get => ClaimType + ":" + ClaimValue;
            set{
                string[] arr = value.Split(':');
                ClaimType = arr[0];
                ClaimValue = arr[1];
            }
        }
    }
}