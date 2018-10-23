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
    using System.Threading.Tasks;

    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountDomain domain;

        public AccountController(IAccountDomain domain)
        {
            this.domain = domain;
        }

        [AllowAnonymous]
        public async Task<AjaxModel<ProfileModel>> Validate(LoginModel model)
        {
            return await AjaxHelper.GetAsync(m => this.domain.Validate(model.UserName, model.UserPassword), SecurityMessages.LoginSuccess);
        }

        [Authorize]
        public async Task<AjaxModel<NTModel>> Logout()
        {
            return await AjaxHelper.SaveAsync(m => this.domain.Logout(), SecurityMessages.LogoutSuccess);
        }

        public async Task<AjaxModel<NTModel>> ChangePassword(ChangePasswordModel model)
        {
            return await AjaxHelper.SaveAsync(m => this.domain.ChangePassword(model), SecurityMessages.ChangePasswordSuccess);
        }

        public async Task<AjaxModel<ProfileModel>> ProfileGet()
        {
            return await AjaxHelper.GetAsync(m => this.domain.ProfileGet());
        }
    }
}
