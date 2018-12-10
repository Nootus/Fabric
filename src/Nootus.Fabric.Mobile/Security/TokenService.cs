using Autofac;
using Nootus.Fabric.Mobile.Core;
using Nootus.Fabric.Mobile.Security.Models;
using Nootus.Fabric.Mobile.Settings;
using Nootus.Fabric.Mobile.WebApi;
using Nootus.Fabric.Mobile.WebApi.Models;
using System;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Nootus.Fabric.Mobile.Security
{
    public class TokenService
    {
        private string refreshUri;
        private string refreshProfileUri;

        private readonly Session session;
        private readonly AppSettings appSettings;

        public TokenService(Session session, AppSettings appSettings)
        {
            this.session = session;
            this.appSettings = appSettings;
            session.Token = appSettings.Token;
        }

        public async Task Initialize(string refreshUri, string refreshProfileUri)
        {
            session.Token = session.Token ?? new Token();

            this.refreshUri = refreshUri;
            this.refreshProfileUri = refreshProfileUri;

            if (session.IsAuthenticated)
            {
                if(session.Token.RefreshTokenExpiryDate < DateTime.Now)
                {
                    session.Token = new Token();
                }
                else
                {
                    await RefreshTokenProfile();
                }
            }
        }

        public async Task ExtractTokens(HttpResponseHeaders headers)
        {
            Token token = session.Token;

            if(headers.GetTokenHeader(TokenHttpHeaders.RefreshTokenExpired) == "false")
            {
                // redirect to login page
            }

            if (headers.GetTokenHeader(TokenHttpHeaders.JwtTokenExpired) == "true")
            {
                await RefreshToken();
            }

            token.JwtToken = headers.GetTokenHeader(TokenHttpHeaders.JwtToken) ?? token.JwtToken;
            token.RefreshToken = headers.GetTokenHeader(TokenHttpHeaders.RefreshToken) ?? token.RefreshToken;

            if (headers.Contains(TokenHttpHeaders.RefreshTokenLifeTime))
            {
                token.RefreshTokenExpiryDate = DateTime.Now.AddMinutes(Convert.ToInt32(headers.GetValues(TokenHttpHeaders.RefreshTokenLifeTime).FirstOrDefault()));
            }

            if (token.RefreshToken != null && appSettings.Token?.RefreshToken != token.RefreshToken)
            {
                appSettings.Token = token; // updating the settings
            }
        }

        public async Task RefreshToken()
        {
            await RefreshTokenInternal<NTModel>(refreshUri);
        }

        private async Task RefreshTokenProfile()
        {
            session.UserProfile = await RefreshTokenInternal<UserProfileModel>(refreshProfileUri);
        }

        private async Task<TReturn> RefreshTokenInternal<TReturn>(string uri)
        {
            ApiRequest api = DependencyInjection.Container.Resolve<ApiRequest>();
            RefreshTokenModel refreshModel = new RefreshTokenModel()
            {
                JwtToken = session.Token.JwtToken,
                RefreshToken = session.Token.RefreshToken
            };

            return await api.PostAsync<RefreshTokenModel, TReturn>(refreshProfileUri, refreshModel);
        }
    }
    public static class HeaderExtensions
    {
        public static string GetTokenHeader(this HttpResponseHeaders headers, string name)
        {
            if (headers.Contains(name))
            {
                return headers.GetValues(name).FirstOrDefault();
            }
            else
            {
                return null;
            }
        }
    }
}
