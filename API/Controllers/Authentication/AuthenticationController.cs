using Application.Interfaces.Authentication;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net.Http;

namespace API.Controllers.Authentication
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService ?? throw new ArgumentNullException(nameof(authenticationService));
        }

        [HttpGet("initiate")]
        public async Task<IActionResult> Initiate([FromQuery] string provider, [FromQuery] string redirectUri)
        {
            try
            {
                var url = await _authenticationService.InitiateExternalAuthenticationAsync(provider, redirectUri);
                return Redirect(url);
            }
            catch (ArgumentException ex)
            {
                // Handle exceptions specifically related to the argument validation
                return BadRequest($"An error occurred while initiating authentication: {ex.Message}");
            }
            catch (Exception ex)
            {
                // Handle any other general exceptions
                return StatusCode(500, $"An internal error occurred: {ex.Message}");
            }
        }

        [HttpGet("callback")]
        public async Task<IActionResult> Callback([FromQuery] string provider, [FromQuery] string code)
        {
            try
            {
                var result = await _authenticationService.HandleExternalAuthenticationCallbackAsync(provider, code);
                if (result.IsAuthenticated)
                {
                    // Redirect to a success page or return a successful response
                    return Ok(result);
                }
                else
                {
                    // Return an error response
                    return BadRequest(result.Errors);
                }
            }
            catch (HttpRequestException ex)
            {
                // Handle exceptions specifically related to the HttpRequest
                return BadRequest($"An error occurred while handling the authentication callback: {ex.Message}");
            }
            catch (Exception ex)
            {
                // Handle any other general exceptions
                return StatusCode(500, $"An internal error occurred: {ex.Message}");
            }
        }
    }
}
