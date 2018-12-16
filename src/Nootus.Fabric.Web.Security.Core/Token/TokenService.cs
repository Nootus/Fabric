using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Nootus.Fabric.Web.Core.Context;
using Nootus.Fabric.Web.Security.Core.Common;
using Nootus.Fabric.Web.Security.Core.Identity;
using Nootus.Fabric.Web.Security.Core.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Nootus.Fabric.Web.Security.Core.Token
{
    public static class TokenService
    {
        public static string GenerateJwtToken(UserProfileModel model)
        {             
            Claim[] claims = new[] {
                new Claim(ClaimTypes.NameIdentifier, model.UserId),
                new Claim(ClaimTypes.Name, model.UserName),
                new Claim(NTClaimTypes.FirstName, model.FirstName),
                new Claim(NTClaimTypes.LastName, model.LastName),
                new Claim(NTClaimTypes.CompanyId, model.CompanyId.ToString()),
                new Claim(NTClaimTypes.Companies, model.CompanyId.ToString()),
                new Claim(NTClaimTypes.Roles, String.Join(",", model.Roles.Select(r => r.Name).ToArray()))
               };

            return GenerateJwtToken(claims);
        }

        public static string GenerateJwtToken(ClaimsPrincipal principal)
        {
            string[] claimTypes = new string[] {ClaimTypes.NameIdentifier, ClaimTypes.Name, NTClaimTypes.FirstName,
                                    NTClaimTypes.LastName, NTClaimTypes.CompanyId, NTClaimTypes.Companies, NTClaimTypes.Roles };

            Claim[] claims = principal.Claims.Where(c => claimTypes.Contains(c.Type)).Select(c => new Claim(c.Type, c.Value)).ToArray();

            return GenerateJwtToken(claims);
        }

        public static string GenerateJwtToken(Claim[] claims)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(TokenSettings.SymmetricKey));

            var token = new JwtSecurityToken(issuer: TokenSettings.Issuer,
              audience: TokenSettings.Issuer,
              claims: claims,
              notBefore: DateTime.Now,
              expires: DateTime.Now.AddMinutes(TokenSettings.LifeTime),
              signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256));

            string jwtToken = new JwtSecurityTokenHandler().WriteToken(token);

            SetTokenHeader(jwtToken);
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
                ClockSkew = TimeSpan.FromMinutes(TokenSettings.ClockSkew),
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(TokenSettings.SymmetricKey))
            };
        }

        public static ClaimsPrincipal GetPrincipalFromToken(string token)
        {
            TokenValidationParameters parameters = TokenService.GetTokenValidationParameters();
            parameters.ValidateLifetime = false; // ignore token expiry date
            ClaimsPrincipal principal = new JwtSecurityTokenHandler().ValidateToken(token, parameters, out SecurityToken securityToken);
            if (!(securityToken is JwtSecurityToken jwtSecurityToken) 
                || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException(SecurityMessages.InvalidToken);

            if((DateTime.UtcNow - securityToken.ValidTo).TotalMinutes > TokenSettings.MaxLifeTime)
            {
                return null;
            }

            return principal;
        }

        public static void SetTokenHeader(string jwtToken, string refreshToken = null)
        {
            SetTokenHeader(new TokenHttpHeader() { JwtToken = jwtToken, RefreshToken = refreshToken });
        }

        public static void SetTokenHeader(bool jwtTokenExpired = false, bool refreshTokenExpired = false)
        {
            SetTokenHeader(new TokenHttpHeader() { JwtTokenExpired = jwtTokenExpired, RefreshTokenExpired = refreshTokenExpired });
        }

        public static void SetTokenHeader(TokenHttpHeader tokenHeader)
        {
            string tokenHeaderName = "Token";

            if(tokenHeader.JwtToken != null)
            {
                tokenHeader.JwtLifeTime = TokenSettings.LifeTime;
                tokenHeader.MaxLifeTime = TokenSettings.MaxLifeTime;
            }

            if(NTContext.HttpContext.Response.Headers.Keys.Contains(tokenHeaderName))
            {
                NTContext.HttpContext.Response.Headers.Remove(tokenHeaderName);
            }

            NTContext.HttpContext.Response.Headers.Add(tokenHeaderName, JsonConvert.SerializeObject(tokenHeader));
        }
    }
}
