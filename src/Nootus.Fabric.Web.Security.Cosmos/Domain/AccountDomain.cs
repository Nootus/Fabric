using Nootus.Fabric.Web.Core.Context;
using Nootus.Fabric.Web.Core.Cosmos.Models;
using Nootus.Fabric.Web.Security.Core.Domain;
using Nootus.Fabric.Web.Security.Core.Models;
using Nootus.Fabric.Web.Security.Core.Token;
using Nootus.Fabric.Web.Security.Cosmos.Repositories;
using System.Threading.Tasks;

namespace Nootus.Fabric.Web.Security.Cosmos.Domain
{
    public class AccountDomain : IAccountDomain
    {
        private readonly AccountRepository repository;

        public AccountDomain(AccountRepository repository)
        {
            this.repository = repository;
        }

        public Task ChangePassword(ChangePasswordModel model)
        {
            throw new System.NotImplementedException();
        }

        public async Task Logout()
        {
            // there is no logout for JWT Tokens
            await Task.CompletedTask;
        }

        public async Task<UserProfileModel> ProfileGet()
        {
            return (await repository.UserProfileGet(NTContext.Context.UserName)).Document;
        }

        public Task<UserProfileModel> Register(RegisterUserModel model)
        {
            throw new System.NotImplementedException();
        }

        public async Task<UserProfileModel> Validate(string userName, string password)
        {
            // check user name and password from database
            SharedCollectionDocument<UserProfileModel> document = await repository.Validate(userName, password);
            UserProfileModel model = document.Document;

            // creating tokens
            string jwtToken = TokenService.GenerateJwtToken(model);

            // if no refresh token exists, then create one
            if (string.IsNullOrEmpty(model.RefreshToken))
            {
                model.RefreshToken = TokenService.GenerateRefreshToken();
                await repository.UserProfileSave(document);
            }

            return await Task.FromResult(model);
        }

        public async Task<UserProfileModel> RefreshToken(string jwtToken, string refreshToken)
        {
            // getting user name from token
            string userName = TokenService.GetUsernameFromExpiredToken(jwtToken);

            // getting profile from database
            UserProfileModel model = (await repository.UserProfileGet(userName)).Document;

            // checking refresh token validity
            if(model.RefreshToken == refreshToken)
            {
                TokenService.GenerateJwtToken(model);
            }

            return model;
        }
    }
}
