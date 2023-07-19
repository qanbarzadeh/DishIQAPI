using Application.DTO.RecipeDTOs;
using Application.Interfaces.UserRepo;
using Application.Interfaces.Authentication.Manual;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UserRecipesController : ControllerBase
    {
        private readonly IUserSpecificRecipeStorageService _userSpecificRecipeStorageService;
        private readonly IUserResolverService _userResolverService;

        public UserRecipesController(
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
