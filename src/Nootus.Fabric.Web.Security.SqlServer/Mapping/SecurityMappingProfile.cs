//-------------------------------------------------------------------------------------------------
// <copyright file="SecurityMappingProfile.cs" company="Nootus">
//  Copyright (c) Nootus. All rights reserved.
// </copyright>
// <description>
//  AutoMapper profile for mapping of entities and models in security assembly
// </description>
//-------------------------------------------------------------------------------------------------
namespace Nootus.Fabric.Web.Security.SqlServer.Mapping
{
    using AutoMapper;
    using Microsoft.AspNetCore.Identity;
    using Nootus.Fabric.Web.Core.Exception;
    using Nootus.Fabric.Web.Security.Core.Models;
    using Nootus.Fabric.Web.Security.SqlServer.Entities;

    public class SecurityMappingProfile : Profile
    {
        public SecurityMappingProfile()
        {
            this.CreateMap<CompanyEntity, CompanyModel>();
            this.CreateMap<PageEntity, PageModel>(MemberList.Destination);
            this.CreateMap<PageClaimEntity, PageClaimModel>();
            this.CreateMap<ClaimEntity, ClaimModel>();
            this.CreateMap<MenuPageEntity, MenuModel>();
            this.CreateMap<IdentityRoleClaim<string>, ClaimModel>();
            this.CreateMap<IdentityUserClaim<string>, ClaimModel>();
            this.CreateMap<ApplicationRole, RoleModel>(MemberList.Destination);
            this.CreateMap<IdentityError, NTError>();
        }

        public override string ProfileName
        {
            get
            {
                return "SecurityMappingProfile";
            }
        }
    }
}
