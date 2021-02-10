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
    public class ChuckNorrisService : IChuckNorrisService
    {
        private readonly UrlsConfig _urls;
        private readonly IHttpClientService _httpClientService;

        public ChuckNorrisService(IHttpClientService httpClientService, IOptions<UrlsConfig> config)
        {
            _httpClientService = httpClientService;
            _urls = config.Value;
        }

        public async Task<String[]> GetCategoriesAsync()
        {
            var responseMessage = await _httpClientService.GetAsync(_urls.Chucknorris + UrlsConfig.ChuckNorrisOperations.GetAllCategories());
            Task<string> response = responseMessage.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<String[]>(response.Result);
        }

        public async Task<JokeData> SearchJokesAsync(string query)
        {

            HttpResponseMessage response
                = await _httpClientService.GetAsync(_urls.Chucknorris + UrlsConfig.ChuckNorrisOperations.SearchJokes(query));


            Task<string> stringContent = response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<JokeData>(stringContent.Result);
        }
    }
}
