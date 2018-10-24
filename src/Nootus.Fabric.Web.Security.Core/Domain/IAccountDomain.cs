using Nootus.Fabric.Web.Security.Core.Models;
using System.Threading.Tasks;

namespace Nootus.Fabric.Web.Security.Core.Domain
{
    public interface IAccountDomain
    {
        Task<ProfileModel> Register(RegisterUserModel model);
        Task<ProfileModel> Validate(string userName, string password);
        Task<ProfileModel> ProfileGet();
        Task Logout();
        Task ChangePassword(ChangePasswordModel model);
        Task<ProfileModel> RefreshToken(string jwtToken, string refreshToken);
    }
}
