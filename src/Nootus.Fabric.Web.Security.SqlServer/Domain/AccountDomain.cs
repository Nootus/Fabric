//-------------------------------------------------------------------------------------------------
// <copyright file="AccountDomain.cs" company="Nootus">
//  Copyright (c) Nootus. All rights reserved.
// </copyright>
// <description>
//  Domain class for all security related business logic
// </description>
//-------------------------------------------------------------------------------------------------
namespace Nootus.Fabric.Web.Security.SqlServer.Domain
{
    using Microsoft.AspNetCore.Identity;
    using Nootus.Fabric.Web.Core.Context;
    using Nootus.Fabric.Web.Core.Exception;
    using Nootus.Fabric.Web.Security.Core.Common;
    using Nootus.Fabric.Web.Security.Core.Domain;
    using Nootus.Fabric.Web.Security.Core.Models;
    using Nootus.Fabric.Web.Security.Identity;
    using Nootus.Fabric.Web.Security.SqlServer.Entities;
    using Nootus.Fabric.Web.Security.SqlServer.Repositories;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class AccountDomain: IAccountService
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly SecurityRepository accountRepository;

        public AccountDomain(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, SecurityRepository accountRepository)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.accountRepository = accountRepository;
        }

        public async Task<UserProfileModel> Register(RegisterUserModel model)
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

            return await this.Validate(new LoginModel() { UserName = model.UserName, UserPassword = model.Password });
        }

        public async Task<UserProfileModel> Validate(LoginModel login)
        {
            var result = await this.signInManager.PasswordSignInAsync(login.UserName, login.UserPassword, false, false);

            if (!result.Succeeded)
            {
                throw new NTException(SecurityMessages.InvalidUsernamePassword);
            }

            return await UserProfile.Get(login.UserName, this.accountRepository);
        }

        public async Task<UserProfileModel> ProfileGet()
        {
            return await UserProfile.Get(NTContext.Context.UserName, this.accountRepository);
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

        public Task RefreshToken(string jwtToken, string refreshToken)
        {
            throw new System.NotImplementedException();
        }

        //public async Task<int[]> GetGroupCompanyIds()
        //{
        //    return await this.accountRepository.GetGroupCompanyIds();
        //}
    }
}
