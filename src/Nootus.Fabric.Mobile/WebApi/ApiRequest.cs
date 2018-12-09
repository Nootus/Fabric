using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Nootus.Fabric.Mobile.Dialog;
using Nootus.Fabric.Mobile.Exception;
using Nootus.Fabric.Mobile.Security;
using Nootus.Fabric.Mobile.Settings;
using Nootus.Fabric.Mobile.WebApi.Models;
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
        private readonly Session session;
        private readonly TokenService tokenService;
        private IDialogService dialogService;

        private string refreshApi = "";

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
            dialogService.DisplayLoading();
            HttpClient httpClient = CreateHttpClient();
            HttpResponseMessage response = await httpClient.GetAsync(uri);

            TResult result = await ProcessResponse<TResult>(response);
            dialogService.Hide();
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
            dialogService.DisplayLoading();
            HttpClient httpClient = CreateHttpClient();
            HttpResponseMessage response;
            try
            {
                response = await httpClient.PostAsync(uri, content);
                TResult result = await ProcessResponse<TResult>(response);
                return result;
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
            if(session.Token == null)
            {
                session.Token = settings.Token ?? new Token();
            }

            Token token = session.Token;
            if (token != null)
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.JwtToken);
            }
            return httpClient;
        }

        private async Task<TResult> ProcessResponse<TResult>(HttpResponseMessage response)
        {
            await tokenService.ExtractTokens(response.Headers);

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

    }
}
