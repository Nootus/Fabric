using Microsoft.IdentityModel.Tokens;
using Nootus.Fabric.Web.Core.Context;
using Nootus.Fabric.Web.Security.Core.Identity;
using Nootus.Fabric.Web.Security.Core.Models;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Nootus.Fabric.Web.Security.Core.Token
{
    public static class TokenService
    {
        public static string GenerateJwtToken(ProfileModel model)
        {            
            var claims = new[] {
                new Claim(ClaimTypes.NameIdentifier, model.UserId),
                new Claim(ClaimTypes.Name, model.UserName),
                new Claim(NTClaimTypes.FirstName, model.FirstName),
                new Claim(NTClaimTypes.LastName, model.LastName),
                new Claim(NTClaimTypes.CompanyId, model.CompanyId.ToString()),
               };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(TokenSettings.SymmetricKey));
            
            var token = new JwtSecurityToken(issuer: TokenSettings.Issuer,
              audience: TokenSettings.Issuer,
              claims: claims,
              expires: DateTime.Now.AddMinutes(TokenSettings.Duration),
              signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256));

            string jwtToken = new JwtSecurityTokenHandler().WriteToken(token);
            NTContext.HttpContext.Response.Headers.Add("JwtToken", jwtToken);
            return jwtToken;
        }

        public static string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
            }

            string refreshToken = Convert.ToBase64String(randomNumber);
            return refreshToken;
        }

        public static TokenValidationParameters GetTokenValidationParameters()
        {
            return new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = TokenSettings.Issuer,
                ValidAudience = TokenSettings.Issuer,
                ClockSkew = TimeSpan.Zero,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(TokenSettings.SymmetricKey))
            };
        }

        public static string GetUsernameFromExpiredToken(string token)
        {
            TokenValidationParameters parameters = TokenService.GetTokenValidationParameters();
            parameters.ValidateLifetime = false; // ignore token expiry date
            ClaimsPrincipal principal = new JwtSecurityTokenHandler().ValidateToken(token, parameters, out SecurityToken securityToken);
            if (!(securityToken is JwtSecurityToken jwtSecurityToken) || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid token");

            return principal.Identity.Name;
        }
    }
}
