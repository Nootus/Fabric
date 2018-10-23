using Nootus.Fabric.Web.Security.Core.Models;
using System.Threading.Tasks;

namespace Nootus.Fabric.Web.Security.Cosmos.Repositories
{
    public class AccountRepository
    {
        public async Task<ProfileModel> Validate(string userName, string password)
        {
            return await GetProfileModel(userName);
        }

        public async Task<ProfileModel> GetProfileModel(string userName)
        {
            ProfileModel model = new ProfileModel()
            {
                UserId = "123",
                UserName = "Prasanna@Nootus.com",
                FirstName = "Prasanna",
                LastName = "Pattam",
                CompanyId = 0
            };

            return await Task.FromResult(model);
        }
    }
}
