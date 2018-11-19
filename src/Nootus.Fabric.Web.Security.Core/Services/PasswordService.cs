using Microsoft.AspNetCore.Identity;
using Nootus.Fabric.Web.Security.Core.Models;
using System;
using System.Text;

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

        public static string GeneratePassword()
        {
            var options = new PasswordOptions();

            bool nonAlphanumeric = options.RequireNonAlphanumeric;
            bool digit = options.RequireDigit;
            bool lowercase = options.RequireLowercase;
            bool uppercase = options.RequireUppercase;

            StringBuilder password = new StringBuilder();
            Random random = new Random();

            while (password.Length < options.RequiredLength)
            {
                char c = (char)random.Next(32, 126);

                password.Append(c);

                if (char.IsDigit(c))
                    digit = false;
                else if (char.IsLower(c))
                    lowercase = false;
                else if (char.IsUpper(c))
                    uppercase = false;
                else if (!char.IsLetterOrDigit(c))
                    nonAlphanumeric = false;
            }

            if (nonAlphanumeric)
                password.Append((char)random.Next(33, 48));
            if (digit)
                password.Append((char)random.Next(48, 58));
            if (lowercase)
                password.Append((char)random.Next(97, 123));
            if (uppercase)
                password.Append((char)random.Next(65, 91));

            return password.ToString();
        }

        public static string DefaultPassword => HashPassword(GeneratePassword());
    }
}
