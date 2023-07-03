using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Identity.Client;
using Microsoft.Identity.Web;

namespace API.Controllers.Authentication.Microsoft.IdnetityWeb
{
    [ApiController]
    [Route("[controller]")]
    public class AuthenticationController_0 : ControllerBase
    {
        private readonly ITokenAcquisition _tokenAcquisition;
        private readonly IConfiguration _configuration;

        public AuthenticationController_0(ITokenAcquisition tokenAcquisition, IConfiguration configuration)
        {
            _tokenAcquisition = tokenAcquisition;
            _configuration = configuration;
        }

        [HttpGet("login")]
        public IActionResult Login()
        {
            var properties = new AuthenticationProperties()
            {
                RedirectUri = Url.Action("signin-oidc", "Authentication", null, Request.Scheme),
                AllowRefresh = true,
                IsPersistent = true
            };

            return Challenge(properties, OpenIdConnectDefaults.AuthenticationScheme);
        }

        [HttpGet("signin-oidc")]
        public async Task<IActionResult> Redirect([FromQuery] string code)
        {
            try
            {
                var scopes = _configuration["AzureAd:Scopes"].Split(' ');

                // Specify the scheme as OpenIdConnect
                var result = await _tokenAcquisition.GetAuthenticationResultForUserAsync(scopes, tenantId: null, userFlow: null, user: User, authenticationScheme: OpenIdConnectDefaults.AuthenticationScheme);

                return Ok(new { accessToken = result.AccessToken });
            }
            catch (Exception ex)
            {
                // Log the exception with a logger (e.g., ILogger) here if needed.
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}
