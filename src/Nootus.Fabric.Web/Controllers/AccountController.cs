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
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Nootus.Fabric.Web.Core.Helpers.Web;
    using Nootus.Fabric.Web.Core.Models.Web;
    using Nootus.Fabric.Web.Security.Common;
    using Nootus.Fabric.Web.Security.Domain;
    using Nootus.Fabric.Web.Security.Models;

    public class AccountController : Controller
    {
        private readonly AccountDomain domain;

        public AccountController(AccountDomain domain)
        {
            this.domain = domain;
        }

        [HttpPost]
        public async Task<AjaxModel<ProfileModel>> Validate([FromBody] LoginModel model)
        {
            return await AjaxHelper.GetAsync(m => this.domain.Validate(model.UserName, model.UserPassword), SecurityMessages.LoginSuccess);
        }

        [HttpGet]
        public async Task<AjaxModel<NTModel>> Logout()
        {
            return await AjaxHelper.SaveAsync(m => this.domain.Logout(), SecurityMessages.LogoutSuccess);
        }

        [HttpPost]
        public async Task<AjaxModel<NTModel>> ChangePassword([FromBody] ChangePasswordModel model)
        {
            return await AjaxHelper.SaveAsync(m => this.domain.ChangePassword(model), SecurityMessages.ChangePasswordSuccess);
        }

        [HttpGet]
        public async Task<AjaxModel<ProfileModel>> ProfileGet()
        {
            return await AjaxHelper.GetAsync(m => this.domain.ProfileGet());
        }
    }
}
