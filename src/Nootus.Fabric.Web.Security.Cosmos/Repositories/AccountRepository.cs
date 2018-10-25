using Nootus.Fabric.Web.Core.Cosmos.Models;
using Nootus.Fabric.Web.Security.Core.Models;
using System.Threading.Tasks;

namespace Nootus.Fabric.Web.Security.Cosmos.Repositories
{
    public class AccountRepository
    {
        private static UserProfileModel Model = new UserProfileModel()
        {
            UserId = "123",
            UserName = "Prasanna@Nootus.com",
            FirstName = "Prasanna",
            LastName = "Pattam",
            CompanyId = 0
        };

        public async Task<SharedCollectionDocument<UserProfileModel>> Validate(string userName, string password)
        {
            return await UserProfileGet(userName);
        }

        public async Task<SharedCollectionDocument<UserProfileModel>> UserProfileGet(string userName)
        {
            SharedCollectionDocument<UserProfileModel> document = new SharedCollectionDocument<UserProfileModel>()
            {
                Document = AccountRepository.Model
        };

            return await Task.FromResult(document);
        }

        public async Task UserProfileSave(SharedCollectionDocument<UserProfileModel> document)
        {            
            await Task.CompletedTask;
        }
    }
}
