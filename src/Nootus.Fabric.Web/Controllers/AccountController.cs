//-------------------------------------------------------------------------------------------------
// <copyright file="AccountController.cs" company="Nootus">
//  Copyright (c) Nootus. All rights reserved.
// </copyright>
// <description>
//  User security related functionality
// </description>
//-------------------------------------------------------------------------------------------------
namespace Nootus.Fabric.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Nootus.Fabric.Web.Core.Helpers.Web;
    using Nootus.Fabric.Web.Core.Models.Web;
    using Nootus.Fabric.Web.Security.Core.Common;
    using Nootus.Fabric.Web.Security.Core.Domain;
    using Nootus.Fabric.Web.Security.Core.Models;
    using System;
    using System.Threading.Tasks;

    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService domain;

        public AccountController(IAccountService domain)
            => this.domain = domain;
        

        public async Task<AjaxModel<UserProfileModel>> Validate(LoginModel model)
            => await AjaxHelper.GetAsync(m => this.domain.Validate(model), SecurityMessages.LoginSuccess);
        
        public async Task<AjaxModel<NTModel>> Logout()
            => await AjaxHelper.SaveAsync(m => this.domain.Logout(), SecurityMessages.LogoutSuccess);
        
        public async Task<AjaxModel<NTModel>> ChangePassword(ChangePasswordModel model)
            => await AjaxHelper.SaveAsync(m => this.domain.ChangePassword(model), SecurityMessages.ChangePasswordSuccess);       

        public async Task<AjaxModel<UserProfileModel>> ProfileGet()
            => await AjaxHelper.GetAsync(m => this.domain.ProfileGet());
        
        public async Task<AjaxModel<NTModel>> RefreshToken(RefreshTokenModel model)
            => await AjaxHelper.SaveAsync(m => domain.RefreshToken(model.JwtToken, model.RefreshToken), String.Empty);
        
        public async Task<AjaxModel<UserProfileModel>> RefreshTokenAndProfileGet(RefreshTokenModel model)
            => await AjaxHelper.GetAsync(async m => {
                    await domain.RefreshToken(model.JwtToken, model.RefreshToken);
                   return await domain.ProfileGet();
               });
        
    }
}
