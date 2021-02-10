using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Open.API.Services.ResourceClient
{
    public class HttpClientService :  IHttpClientService
    {
        private HttpClient _client;

        public HttpClientService(HttpClient httpClient)
        {
            _client = httpClient;
            ConfigurateHttpClient();
        }

        public async Task<HttpResponseMessage> GetAsync(string uri)
        {
            HttpResponseMessage response = await _client.GetAsync(uri);
            if (!response.IsSuccessStatusCode)
            {
                string msg = response.Content.ReadAsStringAsync().Result;
                var exception = new Exception(msg);
                exception.Data.Add("StatusCode", response.StatusCode);
                throw exception;
            }
            return response;
        }

        private void ConfigurateHttpClient()
        {
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

    }
}
