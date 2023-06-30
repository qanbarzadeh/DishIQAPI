using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Identity.Client;
using Microsoft.Identity.Web;

namespace API.Controllers.Authentication.Microsoft.IdnetityWeb
{
    [ApiController]
    [Route("[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly ITokenAcquisition _tokenAcquisition;
        private readonly IConfiguration _configuration;

        public AuthenticationController(ITokenAcquisition tokenAcquisition, IConfiguration configuration)
        {
            _tokenAcquisition = tokenAcquisition;
            _configuration = configuration;
        }

        [HttpGet("login")]
        public IActionResult Login()
        {
            var authUrl = $"{_configuration["AzureAd:Instance"]}{_configuration["AzureAd:TenantId"]}/oauth2/v2.0/authorize?" +
                          $"client_id={_configuration["AzureAd:ClientId"]}&" +
                          $"response_type=code&" +
                          $"redirect_uri={Url.Action("signin-oidc", "Authentication", null, Request.Scheme)}&" +
                          $"response_mode=query&" +
                          $"scope=offline_access%20{_configuration["AzureAd:Scopes"]}";

            return Ok(new { authUrl });
        }

        [HttpGet("signin-oidc")]
        public async Task<IActionResult> Redirect([FromQuery] string code)
        {
            try
            {
                IConfidentialClientApplication app = ConfidentialClientApplicationBuilder.Create(_configuration["AzureAd:ClientId"])
                    .WithClientSecret(_configuration["AzureAd:ClientSecret"])
                    .WithAuthority(new Uri($"{_configuration["AzureAd:Instance"]}{_configuration["AzureAd:TenantId"]}"))
                    .Build();

                var result = await app.AcquireTokenByAuthorizationCode(new[] { _configuration["AzureAd:Scopes"] }, code).ExecuteAsync();

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
