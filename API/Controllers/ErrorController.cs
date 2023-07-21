using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ErrorController : ControllerBase
    {
        [HttpGet] // Added HTTP verb here
        [Route("")]
        public IActionResult Error()
        {
            var context = HttpContext.Features.Get<IExceptionHandlerFeature>();
            var exception = context?.Error; // this is the exception that was thrown
            var code = 500; // default to 500

            // Check if the exception is a specific type
            if (exception is System.IO.FileNotFoundException) code = 404; // not found
            else if (exception is System.UnauthorizedAccessException) code = 401; // unauthorized
                                                                                  // ... etc: set the code based on the exception type

            Response.StatusCode = code; // set the status code as per the exception

            return new JsonResult(new
            {
                error = new { message = exception?.Message },
                statusCode = code
            });
        }
    }
}
