//-------------------------------------------------------------------------------------------------
// <copyright file="Profile.cs" company="Nootus">
//  Copyright (c) Nootus. All rights reserved.
// </copyright>
// <description>
//  Extension method which retrieves the profile for a user
// </description>
//-------------------------------------------------------------------------------------------------
namespace Nootus.Fabric.Web.Security.Identity
{
    using System.Linq;
    using System.Threading.Tasks;
    using Nootus.Fabric.Web.Core.Context;
    using Nootus.Fabric.Web.Security.Extensions;
    using Nootus.Fabric.Web.Security.Middleware;
    using Nootus.Fabric.Web.Security.Models;
    using Nootus.Fabric.Web.Security.Repositories;

    public static class Profile
    {
        public static async Task<ProfileModel> Get(string userName, SecurityRepository accountRepository)
        {
            ProfileModel profile;
            string profileContextName = "ProfileContextObject";

            if (NTContext.HttpContext.Items[profileContextName] == null)
            {
                int companyId = NTContext.Context.CompanyId;
                profile = await accountRepository.UserProfileGet(userName, companyId);

                // setting all the roles for admin roles
                if (profile.AdminRoles.Length > 0)
                {
                    profile.AdminRoles = PageService.AdminRoles.Where(r => profile.AdminRoles.Contains(r.Key)).Select(r => r.Item).ToArray();
                }

                profile.SetMenu();

                // setting the claims on to the context
                NTContextModel model = new NTContextModel()
                {
                    UserId = profile.UserId,
                    UserName = profile.UserName,
                    FirstName = profile.FirstName,
                    LastName = profile.LastName,
                    CompanyId = profile.CompanyId,
                };
                NTContext.Context = model;

                NTContext.HttpContext.Items[profileContextName] = profile;
            }
            else
            {
                profile = (ProfileModel)NTContext.HttpContext.Items[profileContextName];
            }

            return profile;
        }
    }
}
