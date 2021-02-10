using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Open.API.Services;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Open.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ChuckNorrisController : Controller
    {
        private readonly ILogger<ChuckNorrisController> _logger;
        private readonly IChuckNorrisService _chuckNorrisService;

        public ChuckNorrisController(IChuckNorrisService chuckNorrisService,ILogger<ChuckNorrisController> logger)
        {
            _logger = logger;
            _chuckNorrisService = chuckNorrisService;
        }

        [HttpGet]
        [Route("categories")]
        [ProducesResponseType(typeof(List<string>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> CategoriesAsync()
        {
            try
            {
                return Ok(await _chuckNorrisService.GetCategoriesAsync());
            }
            catch (Exception ex)
            {
                _logger.LogError($"FAILED: - {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }
    }
}
