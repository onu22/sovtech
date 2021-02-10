using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Open.API.Models;
using Open.API.Services;


// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Open.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/v1/[controller]")]
    public class SearchController : Controller
    {
        private readonly ILogger<SearchController> _logger;
        private readonly ISwapiService _swapiService;
        private readonly IChuckNorrisService _chuckNorrisService;

        public SearchController(IChuckNorrisService chuckNorrisService, ISwapiService swapiService, ILogger<SearchController> logger)
        {
            _logger = logger;
            _swapiService = swapiService;
            _chuckNorrisService = chuckNorrisService;
        }


        [HttpGet]
        [Route("search/{query}")]
        [ProducesResponseType(typeof(SearchData), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> SearchAsync(string query)
        {

            if (string.IsNullOrEmpty(query)) return BadRequest();


            SearchData searchData = new SearchData();
           
            try
            {
                var jokeData = await _chuckNorrisService.SearchJokesAsync(query);
                if (jokeData.Total > 0)
                {
                    searchData.ChuckNorris = jokeData;
                    searchData.SourceApis.Add("chucknorris");
                }


                var swapData = await _swapiService.SearchPeopleAsync(query);
                if (swapData.Count > 0)
                {
                    searchData.SwAPI = swapData;
                    searchData.SourceApis.Add("swapi");
                }

                return Ok(searchData);
            }
            catch (Exception ex)
            {
                _logger.LogError($"FAILED: - {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }


    }
}
