using System;
using System.Threading.Tasks;
using Open.API.Models;

namespace Open.API.Services
{
    public interface ISwapiService
    {

        Task<SwapData> GetPeopleAsync();

        Task<SwapData> SearchPeopleAsync(string query);
    }
}
