using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Open.API.config;
using Open.API.Models;
using Open.API.Services.ResourceClient;

namespace Open.API.Services
{
    public class SwapiService : ISwapiService
    {
        private readonly UrlsConfig _urls;
        private readonly IHttpClientService _httpClientService;


        public SwapiService(IHttpClientService httpClientService, IOptions<UrlsConfig> config)
        {
            _httpClientService = httpClientService;
            _urls = config.Value;
        }

        public async Task<SwapData> GetPeopleAsync()
        {
            HttpResponseMessage responseMessage = await _httpClientService.GetAsync(_urls.Swapi + UrlsConfig.SwapAPIOperations.GetPeople());
            Task<string> stringContent = responseMessage.Content.ReadAsStringAsync();

            var result =  JsonConvert.DeserializeObject<SwapData>(stringContent.Result);
            return result;
        }

        public async Task<SwapData> SearchPeopleAsync(string query)
        {
            HttpResponseMessage responseMessage
              = await _httpClientService.GetAsync(_urls.Swapi + UrlsConfig.SwapAPIOperations.SearchPeople(query));


            Task<string> stringContent = responseMessage.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<SwapData>(stringContent.Result);
            return result;
        }
    }
}
