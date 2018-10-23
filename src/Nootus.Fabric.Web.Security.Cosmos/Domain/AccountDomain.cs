using System.Threading.Tasks;
using Nootus.Fabric.Web.Core.Context;
using Nootus.Fabric.Web.Security.Core.Domain;
using Nootus.Fabric.Web.Security.Core.Models;
using Nootus.Fabric.Web.Security.Cosmos.Common;
using Nootus.Fabric.Web.Security.Cosmos.Repositories;

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

        public Task<ProfileModel> ProfileGet()
        {
            throw new System.NotImplementedException();
        }

        public Task<ProfileModel> Register(RegisterUserModel model)
        {
            throw new System.NotImplementedException();
        }

        public async Task<ProfileModel> Validate(string userName, string password)
        {
            // check user name and password from database
            ProfileModel model = await repository.Validate(userName, password);

            // creating tokens
            string jwtToken = TokenService.GenerateJwtToken(model);

            // if no refresh token exists, then create one
            string refreshToken = TokenService.GenerateRefreshToken();

            return await Task.FromResult(model);
        }

        public async Task<ProfileModel> RefreshToken(string jwtToken, string refreshToken)
        {
            // getting user name from token
            string userName = TokenService.GetUsernameFromExpiredToken(jwtToken);

            // getting profile from database
            ProfileModel model = await repository.GetProfileModel(userName);

            // checking refresh token validity

            return model;
        }
    }
}
