using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Nootus.Fabric.Mobile.Settings;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Nootus.Fabric.Mobile.WebApi
{
    public class ApiRequest
    {
        private readonly JsonSerializerSettings serializerSettings;
        private readonly AppSettings settings;
        public ApiRequest(AppSettings settings)
        {
            serializerSettings = new JsonSerializerSettings
            {
                DateTimeZoneHandling = DateTimeZoneHandling.Utc,
                NullValueHandling = NullValueHandling.Ignore
            };
            serializerSettings.Converters.Add(new StringEnumConverter());
        }

        public async Task<TResult> GetAsync<TResult>(string uri)
        {
            HttpClient httpClient = CreateHttpClient();
            HttpResponseMessage response = await httpClient.GetAsync(uri);

            await HandleResponse(response);
            string serialized = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<TResult>(serialized, serializerSettings);
        }

        public async Task<TResult> PostAsync<TResult>(string uri, TResult data)
        {
            HttpClient httpClient = CreateHttpClient();

            var content = new StringContent(JsonConvert.SerializeObject(data));
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            HttpResponseMessage response = await httpClient.PostAsync(uri, content);

            await HandleResponse(response);
            string serialized = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<TResult>(serialized, serializerSettings);
        }


        private HttpClient CreateHttpClient()
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            string token = settings.JwtToken;
            if (!string.IsNullOrEmpty(token))
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
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
            if (headers.Contains(AppSettings.AuthToken))
            {
                settings.JwtToken = headers.GetValues(AppSettings.AuthToken).FirstOrDefault();
            }
            if (headers.Contains(AppSettings.AuthRefreshToken))
            {
                settings.RefreshToken = headers.GetValues(AppSettings.AuthRefreshToken).FirstOrDefault();
            }
        }
    }
}
