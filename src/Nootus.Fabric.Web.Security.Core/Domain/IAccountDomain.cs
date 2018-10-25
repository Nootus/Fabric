using Nootus.Fabric.Web.Security.Core.Models;
using System.Threading.Tasks;

namespace Nootus.Fabric.Web.Security.Core.Domain
{
    public interface IAccountDomain
    {
        Task<UserProfileModel> Register(RegisterUserModel model);
        Task<UserProfileModel> Validate(string userName, string password);
        Task<UserProfileModel> ProfileGet();
        Task Logout();
        Task ChangePassword(ChangePasswordModel model);
        Task<UserProfileModel> RefreshToken(string jwtToken, string refreshToken);
    }
}
