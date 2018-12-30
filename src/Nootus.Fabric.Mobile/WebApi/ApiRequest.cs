using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Nootus.Fabric.Mobile.Dialog;
using Nootus.Fabric.Mobile.Exception;
using Nootus.Fabric.Mobile.Security;
using Nootus.Fabric.Mobile.Services;
using Nootus.Fabric.Mobile.Settings;
using Nootus.Fabric.Mobile.WebApi.Models;
using Polly;
using System;
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
         private readonly Session session;
        private readonly TokenService tokenService;
        private IDialogService dialogService;

        public ApiRequest(AppSettings settings, Session session, TokenService tokenService)
        {
            serializerSettings = new JsonSerializerSettings
            {
                DateTimeZoneHandling = DateTimeZoneHandling.Utc,
                NullValueHandling = NullValueHandling.Ignore
            };
            serializerSettings.Converters.Add(new StringEnumConverter());

            this.settings = settings;
            this.session = session;
            this.tokenService = tokenService;
            dialogService = DependencyService.Get<IDialogService>();
        }

        public async Task<TResult> GetAsync<TResult>(string uri)
        {
            return await GetPostAsync<TResult>(HttpMethod.Get, uri, null);
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
            return await GetPostAsync<TResult>(HttpMethod.Post, uri, content);
        }

        private async Task<TResult> GetPostAsync<TResult>(HttpMethod method, string uri, HttpContent content)
        {
            dialogService.DisplayLoading();
            try
            {            
                return await Policy
                    .Handle<HttpRequestException>()
                    .RetryAsync(1)
                    .ExecuteAsync<TResult>(async () =>
                    {
                        HttpClient httpClient = CreateHttpClient();
                        if(settings.HttpClientTimeout > 0)
                            httpClient.Timeout = TimeSpan.FromSeconds(settings.HttpClientTimeout);
                        HttpResponseMessage response = method == HttpMethod.Post ?
                            await httpClient.PostAsync(uri, content) : await httpClient.GetAsync(uri);
                        return await ProcessResponse<TResult>(response);
                    });
            }
            finally
            {
                dialogService.Hide();
            }
        }

        private HttpClient CreateHttpClient()
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            if (session.Token.JwtToken != null)
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", session.Token.JwtToken);
            }
            return httpClient;
        }

        private async Task<TResult> ProcessResponse<TResult>(HttpResponseMessage response)
        {
            await tokenService.ExtractTokens(response.Headers);

            if (!response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();

                switch (response.StatusCode)
                {
                    case HttpStatusCode.Unauthorized:
                        throw new HttpRequestException(content);

                    case HttpStatusCode.Forbidden:
                        throw new System.Exception(content);

                    default:
                        throw new HttpRequestException(content);
                }
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

                if (!string.IsNullOrEmpty(ajax.Message))
                {
                    DependencyService.Get<IToastService>().LongToast(ajax.Message);
                }

                return ajax.Model;
            }
        }
    }
}
