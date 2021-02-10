using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Open.API.config;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Open.API.Controllers
{

    [ApiController]
    [Route("[controller]")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class AccountController : Controller
    {
        private readonly AdClientConfig _adClientConfig;

        public AccountController(IOptions<AdClientConfig> adClientConfig)
        {
            _adClientConfig = adClientConfig.Value;
        }

        [HttpGet]
        [Route("login")]
        public IActionResult Login()
        {

            return Redirect(_adClientConfig.GetAuthorizeEndpoint());
        }

        [HttpGet]
        [Route("acquireToken")]
        public async Task<IActionResult> AcquireToken([FromQuery] string code)
        {

            string authority = _adClientConfig.GetAuthorityEndpoint();
            AuthenticationContext ac = new AuthenticationContext(authority, new TokenCache());
            try
            {
                ClientCredential cc = new ClientCredential(_adClientConfig.Clientid, _adClientConfig.ClientSecret);
                var response = await ac.AcquireTokenByAuthorizationCodeAsync(code, new Uri(_adClientConfig.RedirectUrl),cc,_adClientConfig.Resource);
                return Ok(response);

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
