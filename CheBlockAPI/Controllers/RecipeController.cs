using Application.DTO.RecipeDTOs;
using Application.Services.Recipe;
using Microsoft.AspNetCore.Mvc;

namespace CheBlockAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipeController : ControllerBase
    {
        
        private readonly IRecipeService _recipeService; 
        public RecipeController(IRecipeService recipeService)
        {
          _recipeService = recipeService;
        }

        [HttpPost]
        public async Task<ActionResult<GeneratedRecipeDTO>> GenerateRecipe(RecipeRequestDTO request)
        {
            var generatedRecipe = await _recipeService.GetGeneratedRecipeAsync(request); 
            
            return Ok(generatedRecipe);
        }
    }
}

