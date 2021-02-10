using System;
using System.Threading.Tasks;
using Open.API.Models;

namespace Open.API.Services
{
    public interface IChuckNorrisService
    {

        Task<String[]> GetCategoriesAsync();

        Task<JokeData> SearchJokesAsync(string query);
    }
}
