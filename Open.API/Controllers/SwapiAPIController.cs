using System;
using System.Collections.Generic;
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
    public class SwapiAPIController : Controller
    {
        private readonly ILogger<SwapiAPIController> _logger;
        private readonly ISwapiService _swapiService;

        public SwapiAPIController(ISwapiService swapiService, ILogger<SwapiAPIController> logger)
        {
            _logger = logger;
            _swapiService = swapiService;
        }


        [HttpGet]
        [Route("people")]
        [ProducesResponseType(typeof(SwapData), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> PeopleAsync()
        {
            try
            {
                return Ok(await _swapiService.GetPeopleAsync());
            }
            catch (Exception ex)
            {
                _logger.LogError($"FAILED: - {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }


    }
}
