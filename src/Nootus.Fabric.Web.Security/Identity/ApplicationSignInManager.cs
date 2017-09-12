//-------------------------------------------------------------------------------------------------
// <copyright file="ApplicationSignInManager.cs" company="Nootus">
//  Copyright (c) Nootus. All rights reserved.
// </copyright>
// <description>
//  Custom SignInManager to store basic claims such as name, companies
// </description>
//-------------------------------------------------------------------------------------------------
namespace Nootus.Fabric.Web.Security.Identity
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Nootus.Fabric.Web.Core.Context;
    using Nootus.Fabric.Web.Security.Entities;
    using Nootus.Fabric.Web.Security.Models;
    using Nootus.Fabric.Web.Security.Repositories;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;
    using Newtonsoft.Json;
    using Microsoft.AspNetCore.Authentication;

    public class ApplicationSignInManager : SignInManager<ApplicationUser>
    {
        private SecurityRepository accountRepository;

        public ApplicationSignInManager(
                SecurityRepository accountRepository,            
                UserManager<ApplicationUser> userManager, 
                IHttpContextAccessor contextAccessor, 
                IUserClaimsPrincipalFactory<ApplicationUser> claimsFactory, 
                IOptions<IdentityOptions> optionsAccessor, 
                ILogger<SignInManager<ApplicationUser>> logger, 
                IAuthenticationSchemeProvider schemes)
            : base(userManager, contextAccessor, claimsFactory, optionsAccessor, logger, schemes)
        {
            this.accountRepository = accountRepository;
        }

        public override async Task<ClaimsPrincipal> CreateUserPrincipalAsync(ApplicationUser user)
        {
            var principal = await base.CreateUserPrincipalAsync(user);
            ClaimsIdentity identity = (ClaimsIdentity)principal.Identity;
            var profile = await Profile.Get(user.UserName, this.accountRepository);

            string companies = string.Join(",", profile.Companies.Select(s => s.CompanyId.ToString()));

            identity.AddClaim(new Claim(NTClaimTypes.FirstName, profile.FirstName));
            identity.AddClaim(new Claim(NTClaimTypes.LastName, profile.LastName));
            identity.AddClaim(new Claim(NTClaimTypes.CompanyId, profile.CompanyId.ToString()));
            identity.AddClaim(new Claim(NTClaimTypes.Companies, companies));

            // storing claims in session and removing them. These claims will be added by Transformer
            List<ClaimModel> sessionClaims = new List<ClaimModel>();
            List<Claim> identityClaims = identity.Claims.ToList();
            foreach (var claim in identityClaims)
            {
                if (claim.Type != ClaimTypes.Name && claim.Type != NTClaimTypes.FirstName && claim.Type != NTClaimTypes.LastName && claim.Type != NTClaimTypes.CompanyId)
                {
                    sessionClaims.Add(new ClaimModel() { ClaimType = claim.Type, ClaimValue = claim.Value });
                    identity.RemoveClaim(claim);
                }
            }

            NTContext.HttpContext.Session.SetString("IdentityClaims", JsonConvert.SerializeObject(sessionClaims));

            return principal;
        }
    }
}