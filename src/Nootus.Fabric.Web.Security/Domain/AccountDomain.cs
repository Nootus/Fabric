﻿//-------------------------------------------------------------------------------------------------
// <copyright file="AccountDomain.cs" company="Nootus">
//  Copyright (c) Nootus. All rights reserved.
// </copyright>
// <description>
//  Domain class for all security related business logic
// </description>
//-------------------------------------------------------------------------------------------------
namespace Nootus.Fabric.Web.Security.Domain
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Identity;
    using Nootus.Fabric.Web.Core.Context;
    using Nootus.Fabric.Web.Core.Exception;
    using Nootus.Fabric.Web.Security.Common;
    using Nootus.Fabric.Web.Security.Entities;
    using Nootus.Fabric.Web.Security.Identity;
    using Nootus.Fabric.Web.Security.Models;
    using Nootus.Fabric.Web.Security.Repositories;

    public class AccountDomain
    {
        private UserManager<ApplicationUser> userManager;
        private SignInManager<ApplicationUser> signInManager;
        private SecurityRepository accountRepository;

        public AccountDomain(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, SecurityRepository accountRepository)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.accountRepository = accountRepository;
        }

        public async Task<ProfileModel> Register(RegisterUserModel model)
        {
            if (model.Password != model.ConfirmPassword)
            {
                throw new NTException(SecurityMessages.PasswordsDifferent);
            }

            IdentityResult result = await this.userManager.CreateAsync(new ApplicationUser() { Email = model.UserName, UserName = model.UserName }, model.Password);
            if (result.Succeeded)
            {
                ApplicationUser user = await this.userManager.FindByNameAsync(model.UserName);
                await this.userManager.AddToRoleAsync(user, SecuritySettings.DefaultNewUserRole);
                await this.accountRepository.UserProfileSave(
                    new UserProfileEntity() { UserProfileId = user.Id, FirstName = model.FirstName, LastName = model.LastName, EmailAddress = user.Email },
                    SecuritySettings.NootusCompanyId);
            }
            else
            {
                throw new NTException(SecurityMessages.RegisterUserError, AutoMapper.Mapper.Map<List<NTError>>(result.Errors));
            }

            return await this.Validate(model.UserName, model.Password);
        }

        public async Task<ProfileModel> Validate(string userName, string password)
        {
            var result = await this.signInManager.PasswordSignInAsync(userName, password, false, false);

            if (!result.Succeeded)
            {
                throw new NTException(SecurityMessages.InvalidUsernamePassword);
            }

            return await Profile.Get(userName, this.accountRepository);
        }

        public async Task<ProfileModel> ProfileGet()
        {
            return await Profile.Get(NTContext.Context.UserName, this.accountRepository);
        }

        public async Task Logout()
        {
            await this.signInManager.SignOutAsync();
        }

        public async Task ChangePassword(ChangePasswordModel model)
        {
            ApplicationUser user = await this.userManager.FindByIdAsync(NTContext.Context.UserId);
            IdentityResult result = await this.userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);

            if (!result.Succeeded)
            {
                throw new NTException(SecurityMessages.ChangePasswordError, AutoMapper.Mapper.Map<List<NTError>>(result.Errors));
            }
        }

        public async Task<int[]> GetGroupCompanyIds()
        {
            return await this.accountRepository.GetGroupCompanyIds();
        }
    }
}
