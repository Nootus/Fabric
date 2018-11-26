using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Nootus.Fabric.Mobile.Security;
using Nootus.Fabric.Mobile.Services;
using Nootus.Fabric.Mobile.Settings;
using Nootus.Fabric.Mobile.Views;
using System;
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
            loadingService.InitializeLoading(new LoadingPage());
        }

        public async Task<TResult> GetAsync<TResult>(string uri)
        {
            loadingService.ShowLoading();
            HttpClient httpClient = CreateHttpClient();
            HttpResponseMessage response = await httpClient.GetAsync(uri);

            await HandleResponse(response);
            string serialized = await response.Content.ReadAsStringAsync();

            loadingService.HideLoading();
            return JsonConvert.DeserializeObject<TResult>(serialized, serializerSettings);
        }

        public async Task<TResult> PostAsync<TResult>(string uri, TResult data)
        {
            // loadingService.ShowLoading();
            HttpClient httpClient = CreateHttpClient();
            var content = new StringContent(JsonConvert.SerializeObject(data));
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            HttpResponseMessage response;
            try
            {
                 response = await httpClient.PostAsync(uri, content);

            }
            catch(Exception exp)
            {
                throw;
            }

            loadingService.HideLoading();
            await HandleResponse(response);
            string serialized = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<TResult>(serialized, serializerSettings);
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

        private async Task HandleResponse(HttpResponseMessage response)
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
        }

        private void ExtractTokens(HttpResponseHeaders headers)
        {
            Token token = settings.Token;
            if (headers.Contains(TokenHttpHeaders.JwtToken))
            {
                token.JwtToken = headers.GetValues(TokenHttpHeaders.JwtToken).FirstOrDefault();
            }
            if (headers.Contains(TokenHttpHeaders.RefreshToken))
            {
                token.RefreshToken = headers.GetValues(TokenHttpHeaders.RefreshToken).FirstOrDefault();
            }
        }
    }
}
