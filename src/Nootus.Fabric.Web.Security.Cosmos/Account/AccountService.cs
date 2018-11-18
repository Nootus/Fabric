using Microsoft.AspNetCore.Identity;
using Nootus.Fabric.Web.Core.Context;
using Nootus.Fabric.Web.Core.Cosmos.Models;
using Nootus.Fabric.Web.Core.Exception;
using Nootus.Fabric.Web.Security.Core.Common;
using Nootus.Fabric.Web.Security.Core.Domain;
using Nootus.Fabric.Web.Security.Core.Models;
using Nootus.Fabric.Web.Security.Core.Services;
using Nootus.Fabric.Web.Security.Core.Token;
using Nootus.Fabric.Web.Security.Cosmos.Models;
using Nootus.Fabric.Web.Security.Cosmos.Repositories;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Nootus.Fabric.Web.Security.Cosmos.Domain
{
    public class AccountService : BaseService<SecurityDbContext>, IAccountService
    {
        public Task ChangePassword(ChangePasswordModel model)
        {
            throw new System.NotImplementedException();
        }

        public async Task Logout()
            => // there is no logout for JWT Tokens
            await Task.CompletedTask;

        public async Task<UserProfileModel> ProfileGet()
            => await ProfileGet(NTContext.Context.UserName);

        public async Task<UserProfileModel> ProfileGet(string username)
            => await DbService.GetModelByKeyAsyc<UserProfileModel>(username, SecurityAppSettings.ServiceSettings.DocumentTypes.UserProfile);

        public Task<UserProfileModel> Register(RegisterUserModel model)
            => throw new System.NotImplementedException();
        
        //public async Task<int> SendOtp(string mobileNumber)
        //{

        //}

        public async Task<UserProfileModel> Validate(LoginModel login)
        {
            // check user name and password from database
            SharedCollectionDocument<UserAuthModel> userAuthDocument = await DbService.GetDocumentByKeyAsyc<UserAuthModel>(login.UserName, SecurityAppSettings.ServiceSettings.DocumentTypes.UserAuth);
            if(userAuthDocument == null)
            {
                throw new NTException(SecurityMessages.InvalidUsernamePassword);
            }

            bool result = PasswordService.VerifyHashedPassword(userAuthDocument.Model.PasswordHash, login.UserPassword);
            if (!result)
            {
                throw new NTException(SecurityMessages.InvalidUsernamePassword);
            }

            return await CreateToken(login.UserName, userAuthDocument);

        }

        private async Task<UserProfileModel> CreateToken(string userName, SharedCollectionDocument<UserAuthModel> userAuthDocument)
        {
            UserProfileModel model = await ProfileGet(userName);

            // creating tokens
            string jwtToken = TokenService.GenerateJwtToken(model);

            // if no refresh token exists, then create one
            if (string.IsNullOrEmpty(userAuthDocument.Model.RefreshToken))
            {
                userAuthDocument.Model.RefreshToken = TokenService.GenerateRefreshToken();
                await DbService.UpdateDocumentAsync(userAuthDocument);
            }
            NTContext.HttpContext.Response.Headers.Add(TokenHttpHeaders.RefreshToken, userAuthDocument.Model.RefreshToken);

            return model;
        }

        public async Task RefreshToken(string jwtToken, string refreshToken)
        {
            // getting user name from token
            ClaimsPrincipal principal = TokenService.GetPrincipalFromToken(jwtToken);
            if(principal != null)
            {
                UserAuthModel authModel = await DbService.GetModelByKeyAsyc<UserAuthModel>(principal.Identity.Name, SecurityAppSettings.ServiceSettings.DocumentTypes.UserAuth);

                // checking refresh token validity
                if (authModel.RefreshToken == refreshToken)
                {
                    TokenService.GenerateJwtToken(principal);
                }
                else
                {
                    NTContext.HttpContext.Response.Headers.Add(TokenHttpHeaders.TokenRefresh, "false");
                }
            }
        }
    }
}
