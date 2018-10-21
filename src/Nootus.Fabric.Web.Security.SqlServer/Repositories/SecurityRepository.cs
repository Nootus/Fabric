//-------------------------------------------------------------------------------------------------
// <copyright file="SecurityRepository.cs" company="Nootus">
//  Copyright (c) Nootus. All rights reserved.
// </copyright>
// <description>
//  All database queries for security related tables
// </description>
//-------------------------------------------------------------------------------------------------
namespace Nootus.Fabric.Web.Security.SqlServer.Repositories
{
    using AutoMapper;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Nootus.Fabric.Web.Core.Models;
    using Nootus.Fabric.Web.Core.SqlServer.Repositories;
    using Nootus.Fabric.Web.Security.Core.Common;
    using Nootus.Fabric.Web.Security.Core.Models;
    using Nootus.Fabric.Web.Security.SqlServer.Entities;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class SecurityRepository : BaseRepository<SecurityDbContext>
    {
        public SecurityRepository(SecurityDbContext dbContext)
        {
            this.DbContext = dbContext;
        }

        public async Task UserProfileSave(UserProfileEntity entity, int defaultCompanyId)
        {
            this.DbContext.Add(entity);
            await this.DbContext.SaveChangesAsync();
            this.DbContext.Add(new UserCompanyEntity() { UserProfileId = entity.UserProfileId, CompanyId = defaultCompanyId });
            await this.DbContext.SaveChangesAsync();
        }

        public async Task<ProfileModel> UserProfileGet(string userName, int companyId)
        {
            var query = from users in this.DbContext.UserProfiles
                        join idn in this.DbContext.Users on users.UserProfileId equals idn.Id
                        where users.DeletedInd == false
                        && idn.UserName == userName
                        select new ProfileModel
                        {
                            UserId = users.UserProfileId,
                            FirstName = users.FirstName,
                            LastName = users.LastName,
                            UserName = idn.UserName,
                            CompanyId = users.CompanyId,
                        };

            ProfileModel model = await query.FirstOrDefaultAsync();
            if (companyId != 0 && model.CompanyId != companyId)
            {
                model.CompanyId = companyId;

                // updating the User Profile
                UserProfileEntity entity = await this.SingleAsync<UserProfileEntity>(model.UserId);
                entity.CompanyId = model.CompanyId;
                await this.UpdateEntity<UserProfileEntity>(entity, false, true);
            }

            var companyQuery = from cmp in this.DbContext.Companies
                               join usrcmp in this.DbContext.UserCompanies on cmp.CompanyId equals usrcmp.CompanyId
                               where usrcmp.UserProfileId == model.UserId
                               && cmp.DeletedInd == false
                               orderby cmp.CompanyName
                               select Mapper.Map<CompanyEntity, CompanyModel>(cmp);
            model.Companies = await companyQuery.ToListAsync();

            // getting roles and claims
            await this.UserProfileGetRolesClaims(model);

            return model;
        }

        public async Task UserProfileGetRolesClaims(ProfileModel model)
        {
            int companyId = model.CompanyId;
            string userId = model.UserId;

            // getting all roles for the current user
            var roleQuery = from userRoles in this.DbContext.UserRoles
                            join roles in this.DbContext.Roles on userRoles.RoleId equals roles.Id
                            where userRoles.UserId == userId
                                && SecuritySettings.SuperGroupAdminRoles.Contains(roles.RoleType)
                            select Mapper.Map<ApplicationRole, RoleModel>(roles);
            var profileRoles = await roleQuery.ToListAsync();

            // checking for superadmin
            if (profileRoles.Any(r => r.RoleType == (int)RoleType.SuperAdmin))
            {
                // getting all companies
                model.Companies = await (from cmp in this.DbContext.Companies where cmp.DeletedInd == false orderby cmp.CompanyName select Mapper.Map<CompanyEntity, CompanyModel>(cmp)).ToListAsync();
                model.AdminRoles = profileRoles.Where(r => r.RoleType == (int)RoleType.SuperAdmin).Select(r => r.Name).ToList();
                model.Claims = new List<ClaimModel>();
            }
            else if (profileRoles.Any(r => r.RoleType == (int)RoleType.GroupAdmin))
            {
                int[] groupCompanyId = profileRoles.Where(r => r.RoleType == (int)RoleType.GroupAdmin).Select(r => r.CompanyId).ToArray();
                var companies = await (from cmp in this.DbContext.Companies
                                       where cmp.DeletedInd == false && ((cmp.ParentCompanyId != null && groupCompanyId.Contains(cmp.ParentCompanyId.Value)) || groupCompanyId.Contains(cmp.CompanyId))
                                       select Mapper.Map<CompanyEntity, CompanyModel>(cmp)).ToListAsync();
                model.Companies = companies.Union(model.Companies).OrderBy(c => c.CompanyName).ToList();
                model.AdminRoles = profileRoles.Where(r => r.RoleType == (int)RoleType.GroupAdmin).Select(r => r.Name).ToList();
                model.Claims = new List<ClaimModel>();
            }

            if (model.AdminRoles == null)
            {
                // getting company admin role
                var companyRoleQuery = from userRoles in this.DbContext.UserRoles
                                       join roles in this.DbContext.Roles on userRoles.RoleId equals roles.Id
                                       where userRoles.UserId == userId
                                           && roles.CompanyId == companyId
                                           && roles.RoleType == (int)RoleType.CompanyAdmin
                                       select roles.Name;
                model.AdminRoles = await companyRoleQuery.ToListAsync();

                // getting roles claims if user is not a company admin
                if (model.AdminRoles.Count > 0)
                {
                    model.Claims = new List<ClaimModel>();
                }
                else
                {
                    var rolesClaimsQuery = from userRoles in this.DbContext.UserRoles
                                           join roles in this.DbContext.Roles on userRoles.RoleId equals roles.Id
                                           join claims in this.DbContext.RoleClaims on userRoles.RoleId equals claims.RoleId
                                           where userRoles.UserId == userId
                                                 && roles.CompanyId == companyId
                                           select Mapper.Map<IdentityRoleClaim<string>, ClaimModel>(claims);

                    var roleClaims = await rolesClaimsQuery.ToListAsync();

                    // getting user specific overrides
                    var userClaimsQuery = from claim in this.DbContext.UserClaims
                                          where claim.UserId == userId
                                          select Mapper.Map<IdentityUserClaim<string>, ClaimModel>(claim);

                    var userClaims = await userClaimsQuery.ToListAsync();

                    // get the deny claims and remove them from the main claims
                    var denyUserClaims = userClaims.Where(c => c.ClaimType.EndsWith(SecuritySettings.DenySuffix)).ToList();
                    var denyRoleClaims = denyUserClaims.Select(c => new ClaimModel() { ClaimType = c.ClaimType.Replace(SecuritySettings.DenySuffix, string.Empty), ClaimValue = c.ClaimValue }).ToList();

                    userClaims = userClaims.Except(denyUserClaims).ToList();
                    roleClaims = roleClaims.Except(denyRoleClaims, new ClaimModelComparer()).ToList();

                    model.Claims = roleClaims.Union(userClaims).ToList();
                }
            }
        }

        public List<PageModel> PagesGet()
        {
            var query = from page in this.DbContext.Pages.Include(c => c.Claims).ThenInclude(a => a.Claim) select page;
            return Mapper.Map<List<PageEntity>, List<PageModel>>(query.ToList());
        }

        public List<MenuModel> MenuPagesGet()
        {
            var query = from menuPage in this.DbContext.MenuPages where menuPage.DeletedInd == false select Mapper.Map<MenuPageEntity, MenuModel>(menuPage);
            return query.ToList();
        }

        public List<CompanyEntity> CompanyClaimsGet()
        {
            var query = from company in this.DbContext.Companies.Include(c => c.Claims).ThenInclude(a => a.Claim) select company;
            return query.ToList();
        }

        public List<ListItem<string, string>> AdminRolesGet()
        {
            var dbroles = (from roles in this.DbContext.RoleHierarchies
                           join role in this.DbContext.Roles on roles.RoleId equals role.Id
                           join child in this.DbContext.Roles on roles.ChildRoleId equals child.Id
                           select new ListItem<string, string>() { Key = role.Name, Item = child.Name }).ToList();

            var hierarchy = dbroles.SelectMany(r => this.GetChildren(dbroles, r)).Distinct(new ListItemStringComparer()).OrderBy(r => r.Key).ToList();
            return hierarchy;
        }

        public async Task<int[]> GetGroupCompanyIds()
        {
            return await (from cmp in this.DbContext.Companies
                          where cmp.CompanyId == this.AppContext.GroupCompanyId || cmp.ParentCompanyId == this.AppContext.GroupCompanyId
                          select cmp.CompanyId).ToArrayAsync();
        }

        private List<ListItem<string, string>> GetChildren(List<ListItem<string, string>> roles, ListItem<string, string> parent)
        {
            var childroles = roles.Where(r => r.Key == parent.Item).SelectMany(r => this.GetChildren(roles, r));
            var newroles = childroles.Select(r => new ListItem<string, string>() { Key = parent.Key, Item = r.Item }).ToList();

            // adding itself
            newroles.Add(new ListItem<string, string>() { Key = parent.Key, Item = parent.Key });
            newroles.Add(new ListItem<string, string>() { Key = parent.Item, Item = parent.Item });
            newroles.Add(parent);
            return newroles;
        }
    }
}