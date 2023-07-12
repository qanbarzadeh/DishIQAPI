using Application.DTO.RecipeDTOs;
using Application.Services.Recipe;
using Microsoft.AspNetCore.Mvc;

namespace CheBlockAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RecipeController : ControllerBase
    {
        private readonly IRecipeService _recipeService;

        public RecipeController(IRecipeService recipeService)
        {
            _recipeService = recipeService ?? throw new ArgumentNullException(nameof(recipeService));
        }

        [HttpPost]
        [ProducesResponseType(typeof(RecipeResponseDTO), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<RecipeResponseDTO>> GenerateRecipe(RecipeRequestDTO request)
        {
            try
            {
                var generatedRecipe = await _recipeService.GetGeneratedRecipeAsync(request);
                return Ok(generatedRecipe);
            }
            catch (Exception ex)
            {
                // Handle any exceptions and return an appropriate error message
                return StatusCode(500, $"An error occurred while generating the recipe: {ex.Message}");
            }
        }
    }
}
