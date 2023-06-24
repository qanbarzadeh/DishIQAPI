using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Authentication
{
    public class RedirectController : Controller
    {
        [HttpGet] 
        [Route("auth-redirect")]
        public IActionResult AuthRedirect()
        {
            return Content("You are now signed up, congrats!", "text/html");
        }
    }
}
