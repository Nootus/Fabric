using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Nootus.Fabric.Web.Security.Core.Token;
using System.Threading.Tasks;

namespace Nootus.Fabric.Web.Security.Core
{
    public static class SecurityStartup
    {
        public static void ConfigureTokenSettings(IConfiguration configuration)
        {
            // Token settings
            TokenSettings.SymmetricKey = configuration.GetValue<string>("Microservices:Security:JWT:SymmetricKey");
            TokenSettings.Issuer = configuration.GetValue<string>("Microservices:Security:JWT:Issuer");
            TokenSettings.Duration = configuration.GetValue<int>("Microservices:Security:JWT:Duration");
        }

        public static void ConfigureTokenServices(IServiceCollection services)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = TokenService.GetTokenValidationParameters();
                    options.Events = new JwtBearerEvents
                    {
                        OnAuthenticationFailed = context =>
                        {
                            if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                            {
                                context.Response.Headers.Add("Token-Expired", "true");
                            }
                            return Task.CompletedTask;
                        }
                    };
                });
        }
    }
}
