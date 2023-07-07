using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

namespace API.Controllers.Authentication.AzureB2C
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IConfidentialClientApplication _app;

        public AuthController()
        {
            _app = ConfidentialClientApplicationBuilder.Create("0d51501c-f157-4ae5-8d35-08ea624ab473")
                .WithClientSecret("KCY8Q~qGWQ.dOYtshDRj2CYYVubHMFo~4nppfb6-")
                .WithB2CAuthority("https://dishiqapp.b2clogin.com/dishiqapp.onmicrosoft.com/B2C_1_SignUpSignInUserFlow")
                .Build();
        }

        [HttpGet("response")]
        public async Task<IActionResult> Response(string code, string error, string error_description)
        {
            if (!string.IsNullOrEmpty(error))
            {
                // Handle the error sent by Azure B2C
                return BadRequest(new { error, error_description });
            }

            try
            {
                // Now we will try to exchange the code for an access token
                var result = await _app.AcquireTokenByAuthorizationCode(new[] { "https://dishiqapp.onmicrosoft.com/0d51501c-f157-4ae5-8d35-08ea624ab473/read" }, code).ExecuteAsync();

                // Here we just return the tokens for simplicity, but you might want to do something else with them,
                // like create a session, store them for later use, etc.
                return Ok(new { AccessToken = result.AccessToken, ExpiresOn = result.ExpiresOn });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}
