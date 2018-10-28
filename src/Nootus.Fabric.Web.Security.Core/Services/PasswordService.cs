using Microsoft.AspNetCore.Identity;
using Nootus.Fabric.Web.Security.Core.Models;

namespace Nootus.Fabric.Web.Security.Core.Services
{
    public static class PasswordService
    {
        public static string HashPassword(string password)
        {
            PasswordHasher<LoginModel> hasher = new PasswordHasher<LoginModel>();
            return hasher.HashPassword(new LoginModel(), password);
        }

        public static bool VerifyHashedPassword(string hashPassword, string password)
        {
            PasswordHasher<LoginModel> hasher = new PasswordHasher<LoginModel>();
            PasswordVerificationResult result = hasher.VerifyHashedPassword(new LoginModel(), hashPassword, password);

            return result == PasswordVerificationResult.Success;
        }
    }
}
