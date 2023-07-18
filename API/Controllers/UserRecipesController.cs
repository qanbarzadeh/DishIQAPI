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
            try
            {
                if (generatedRecipeDto == null)
                {
                    return BadRequest("Generated recipe data is null.");
                }

                // Get the user from the UserManager using the username in the User ClaimsPrincipal
                var username = User.Identity.Name; // This is the "unique_name" claim in your JWT

                if (string.IsNullOrEmpty(username))
                {
                    return Unauthorized("User is not authorized or token is invalid.");
                }

                var applicationUser = await _userManager.FindByNameAsync(username);

                if (applicationUser == null)
                {
                    return NotFound("User not found.");
                }

                await _userSpecificRecipeStorageService.AddUserWithRecipe(applicationUser, generatedRecipeDto);

                return Ok();
            }
            catch (Exception ex)
            {
                // Log the exception message
                // Use your logging mechanism here e.g., NLog, Serilog, or basic Console.WriteLine
                Console.WriteLine(ex.Message);

                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

    }
}
    