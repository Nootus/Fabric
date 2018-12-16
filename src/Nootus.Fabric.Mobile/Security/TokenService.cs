using Autofac;
using Newtonsoft.Json;
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
                await RefreshTokenProfile();
            }
        }

        public async Task ExtractTokens(HttpResponseHeaders headers)
        {
            string tokenHeaderName = "Token";

            if (headers.Contains(tokenHeaderName))
            {
                TokenHttpHeader tokenHeader = JsonConvert.DeserializeObject<TokenHttpHeader>(headers.GetValues(tokenHeaderName).FirstOrDefault());

                if (tokenHeader.RefreshTokenExpired)
                {
                    // redirect to login page
                    session.Token = new Token();
                }

                if (tokenHeader.JwtTokenExpired)
                {
                    await RefreshToken();
                }

                if (tokenHeader.JwtToken != null)
                {
                    Token token = session.Token;
                    token.JwtToken = tokenHeader.JwtToken;
                    token.JwtTokenExpiry = DateTime.Now.AddMinutes(tokenHeader.JwtLifeTime);
                    token.RefreshToken = tokenHeader.RefreshToken ?? token.RefreshToken;
                    token.RefreshTokenExpiry = DateTime.Now.AddMinutes(tokenHeader.MaxLifeTime + tokenHeader.JwtLifeTime);
                    appSettings.Token = token; // updating the settings
                }
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

            return await api.PostAsync<RefreshTokenModel, TReturn>(uri, refreshModel);
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
