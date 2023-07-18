using Application.DTO.RecipeDTOs;
using Application.Interfaces.UserRepo;
using Domain.Entities.UserEntities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UserRecipesController : ControllerBase
    {
        private readonly IUserSpecificRecipeStorageService _userSpecificRecipeStorageService;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserRecipesController(IUserSpecificRecipeStorageService userSpecificRecipeStorageService, UserManager<ApplicationUser> userManager)
        {
            _userSpecificRecipeStorageService = userSpecificRecipeStorageService;
            _userManager = userManager;
        }

        [HttpPost("SaveGeneratedRecipeForUser")]
        public async Task<IActionResult> SaveGeneratedRecipeForUser([FromBody] GeneratedRecipeDTO generatedRecipeDto)
        {
            // Get the user from the UserManager using the username in the User ClaimsPrincipal
            var username = User.Identity.Name; // This is the "unique_name" claim in your JWT
            var applicationUser = await _userManager.FindByNameAsync(username);

            await _userSpecificRecipeStorageService.AddUserWithRecipe(applicationUser, generatedRecipeDto);

            return Ok();
        }
    }
}
