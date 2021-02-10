using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Open.API.Services.ResourceClient
{
    public interface IHttpClientService
    {
        Task<HttpResponseMessage> GetAsync(string uri);
    }
   
}
