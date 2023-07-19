using Application.DTO.RecipeDTOs;
using Application.Interfaces.Authentication.Manual;
using Application.Interfaces.UserRepo;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestController : ControllerBase
    {
        private readonly IUserSpecificRecipeStorageService _userSpecificRecipeStorageService;
        private readonly IUserResolverService _userResolverService;

        public TestController(
            IUserSpecificRecipeStorageService userSpecificRecipeStorageService,
            IUserResolverService userResolverService)
        {
            _userSpecificRecipeStorageService = userSpecificRecipeStorageService;
            _userResolverService = userResolverService;
        }

        [HttpPost]
        public async Task<IActionResult> SaveGeneratedRecipeForUser([FromBody] GeneratedRecipeDTO generatedRecipeDto)
        {
            var applicationUser = await _userResolverService.GetUserFromToken();

            if (applicationUser == null)
            {
                return BadRequest("User not found.");
            }

            await _userSpecificRecipeStorageService.AddUserWithRecipe(applicationUser, generatedRecipeDto);
            return Ok();
        }
    }
}
