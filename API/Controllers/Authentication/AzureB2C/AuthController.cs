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
            _app = ConfidentialClientApplicationBuilder.Create("4d76d959-03f3-4b47-88d1-4792e469339b")
                .WithClientSecret("NZJ8Q~uyDiZf_x49~ITMqL3-AMZb1WIjP8Cq2c1k")
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
                var result = await _app.AcquireTokenByAuthorizationCode(new[] { "https://dishiqapp.onmicrosoft.com/4d76d959-03f3-4b47-88d1-4792e469339b/read" }, code).ExecuteAsync();

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
