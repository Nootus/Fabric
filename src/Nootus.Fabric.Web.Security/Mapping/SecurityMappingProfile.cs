//-------------------------------------------------------------------------------------------------
// <copyright file="SecurityMappingProfile.cs" company="Nootus">
//  Copyright (c) Nootus. All rights reserved.
// </copyright>
// <description>
//  AutoMapper profile for mapping of entities and models in security assembly
// </description>
//-------------------------------------------------------------------------------------------------
namespace Nootus.Fabric.Web.Security.Mapping
{
    using System.Collections.Generic;
    using AutoMapper;
    using Nootus.Fabric.Web.Core.Exception;
    using Nootus.Fabric.Web.Security.Entities;
    using Nootus.Fabric.Web.Security.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

    public class SecurityMappingProfile : Profile
    {
        public SecurityMappingProfile()
        {
            this.CreateMap<CompanyEntity, CompanyModel>();
            this.CreateMap<IdentityPageEntity, PageModel>(MemberList.Destination);
            this.CreateMap<IdentityPageClaimEntity, PageClaimModel>();
            this.CreateMap<IdentityClaimEntity, ClaimModel>();
            this.CreateMap<IdentityMenuPageEntity, MenuModel>();
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
