using Nootus.Fabric.Web.Core.Cosmos.Models;
using Nootus.Fabric.Web.Security.Core.Models;
using System.Threading.Tasks;

namespace Nootus.Fabric.Web.Security.Cosmos.Repositories
{
    public class AccountRepository
    {
        private static ProfileModel Model = new ProfileModel()
        {
            UserId = "123",
            UserName = "Prasanna@Nootus.com",
            FirstName = "Prasanna",
            LastName = "Pattam",
            CompanyId = 0
        };

        public async Task<SharedCollectionDocument<ProfileModel>> Validate(string userName, string password)
        {
            return await UserProfileGet(userName);
        }

        public async Task<SharedCollectionDocument<ProfileModel>> UserProfileGet(string userName)
        {
            SharedCollectionDocument<ProfileModel> document = new SharedCollectionDocument<ProfileModel>()
            {
                Document = AccountRepository.Model
        };

            return await Task.FromResult(document);
        }

        public async Task UserProfileSave(SharedCollectionDocument<ProfileModel> document)
        {            
            await Task.CompletedTask;
        }
    }
}
