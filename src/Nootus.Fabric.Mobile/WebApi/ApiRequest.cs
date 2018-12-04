using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Nootus.Fabric.Mobile.Exception;
using Nootus.Fabric.Mobile.Security;
using Nootus.Fabric.Mobile.Services;
using Nootus.Fabric.Mobile.Settings;
using Nootus.Fabric.Mobile.Views;
using Nootus.Fabric.Mobile.WebApi.Models;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Nootus.Fabric.Mobile.WebApi
{
    public class ApiRequest
    {
        private readonly JsonSerializerSettings serializerSettings;
        private readonly AppSettings settings;
        private ILoadingService loadingService;

        public ApiRequest(AppSettings settings)
        {
            serializerSettings = new JsonSerializerSettings
            {
                DateTimeZoneHandling = DateTimeZoneHandling.Utc,
                NullValueHandling = NullValueHandling.Ignore
            };
            serializerSettings.Converters.Add(new StringEnumConverter());

            this.settings = settings;
            loadingService = DependencyService.Get<ILoadingService>();
        }

        public async Task<TResult> GetAsync<TResult>(string uri)
        {
            loadingService.ShowLoading();
            HttpClient httpClient = CreateHttpClient();
            HttpResponseMessage response = await httpClient.GetAsync(uri);

            TResult result = await ProcessResponse<TResult>(response);
            loadingService.HideLoading();
            return result;
        }

        public async Task<NTModel> PostAsync<TContent>(string uri, TContent data)
        {
            return await PostAsync<TContent, NTModel>(uri, data);
        }

        public async Task<TResult> PostAsync<TContent, TResult>(string uri, TContent data)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(data));
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            return await PostAsync<TResult>(uri, content);
        }

        public async Task<TResult> PostAsync<TResult>(string uri)
        {
            return await PostAsync<TResult>(uri, null);
        }

        public async Task<TResult> PostAsync<TResult>(string uri, HttpContent content)
        {
            loadingService.ShowLoading();
            HttpClient httpClient = CreateHttpClient();
            HttpResponseMessage response;
            try
            {
                response = await httpClient.PostAsync(uri, content);

            }
            catch (System.Exception exp)
            {
                throw;
            }

            TResult result = await ProcessResponse<TResult>(response);
            loadingService.HideLoading();
            return result;
        }


        private HttpClient CreateHttpClient()
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            Token token = settings.Token;
            if (token != null)
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.JwtToken);
                // httpClient.DefaultRequestHeaders.Add(parameter, value);
            }
            return httpClient;
        }

        private async Task<TResult> ProcessResponse<TResult>(HttpResponseMessage response)
        {
            ExtractTokens(response.Headers);

            if (!response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();

                if (response.StatusCode == HttpStatusCode.Forbidden ||
                    response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    throw new HttpRequestException(content);
                }

                throw new HttpRequestException(content);
            }
            else
            {
                string content = await response.Content.ReadAsStringAsync();
                AjaxModel<TResult> ajax = JsonConvert.DeserializeObject<AjaxModel<TResult>>(content, serializerSettings);

                switch (ajax.Result)
                {
                    case AjaxResult.Exception:
                        throw new System.Exception(ajax.Message);

                    case AjaxResult.ValidationException:
                        throw new NTException(ajax.Message, ajax.Errors);
                }

                return ajax.Model;
            }
        }

        private void ExtractTokens(HttpResponseHeaders headers)
        {
            Token token = settings.Token;
            if (token == null) token = new Token();
            if (headers.Contains(TokenHttpHeaders.JwtToken))
            {
                token.JwtToken = headers.GetValues(TokenHttpHeaders.JwtToken).FirstOrDefault();
            }
            if (headers.Contains(TokenHttpHeaders.RefreshToken))
            {
                token.RefreshToken = headers.GetValues(TokenHttpHeaders.RefreshToken).FirstOrDefault();
                settings.Token = token; // updating the refresh token
            }
        }
    }
}
