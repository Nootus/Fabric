using Nootus.Fabric.Web.Security.Core.Models;
using System.Threading.Tasks;

namespace Nootus.Fabric.Web.Security.Core.Domain
{
    public interface IAccountService
    {
        Task<UserProfileModel> Register(RegisterUserModel model);
        Task<UserProfileModel> Validate(LoginModel login);
        Task<UserProfileModel> ProfileGet();
        Task Logout();
        Task ChangePassword(ChangePasswordModel model);
        Task RefreshToken(string jwtToken, string refreshToken);
    }
}
