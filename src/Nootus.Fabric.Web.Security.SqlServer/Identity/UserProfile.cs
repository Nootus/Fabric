//-------------------------------------------------------------------------------------------------
// <copyright file="UserProfile.cs" company="Nootus">
//  Copyright (c) Nootus. All rights reserved.
// </copyright>
// <description>
//  Extension method which retrieves the profile for a user
// </description>
//-------------------------------------------------------------------------------------------------
namespace Nootus.Fabric.Web.Security.Identity
{
    using Nootus.Fabric.Web.Core.Context;
    using Nootus.Fabric.Web.Security.Core.Extensions;
    using Nootus.Fabric.Web.Security.Core.Middleware;
    using Nootus.Fabric.Web.Security.Core.Models;
    using Nootus.Fabric.Web.Security.SqlServer.Repositories;
    using System.Linq;
    using System.Threading.Tasks;

    public static class UserProfile
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
                if (profile.AdminRoles.Count > 0)
                {
                    profile.AdminRoles = PageService.AdminRoles.Where(r => profile.AdminRoles.Contains(r.Key)).Select(r => r.Item).ToList();
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
