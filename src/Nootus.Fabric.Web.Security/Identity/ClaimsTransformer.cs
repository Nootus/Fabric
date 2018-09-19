//-------------------------------------------------------------------------------------------------
// <copyright file="ClaimsTransformer.cs" company="Nootus">
//  Copyright (c) Nootus. All rights reserved.
// </copyright>
// <description>
//  User messages in this project
// </description>
//-------------------------------------------------------------------------------------------------

namespace Nootus.Fabric.Web.Security.Identity
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Http;
    using Newtonsoft.Json;
    using Nootus.Fabric.Web.Core.Common;
    using Nootus.Fabric.Web.Core.Context;
    using Nootus.Fabric.Web.Security.Models;

    public class ClaimsTransformer : IClaimsTransformation
    {
        public Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
        {
            if (FabricSettings.SessionClaims)
            {
                ClaimsIdentity identity = (ClaimsIdentity)principal.Identity;
                string claimString = NTContext.HttpContext.Session.GetString("IdentityClaims");
                if (claimString != null)
                {
                    List<ClaimModel> sessionClaims = JsonConvert.DeserializeObject<List<ClaimModel>>(claimString);
                    identity.AddClaims(sessionClaims.Select(sc => new Claim(sc.ClaimType, sc.ClaimValue)));
                }
            }

            return Task.FromResult(principal);
        }
    }
}
