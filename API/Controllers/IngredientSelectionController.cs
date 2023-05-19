using Application.DTO.IngredientSelection;
using Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]    
    public class IngredientSelectionController : ControllerBase
    {        
        private readonly IIngredientsSelectionService _ingredientSelectionService;

        public IngredientSelectionController(IIngredientsSelectionService ingredientsSelectionService)
        {
            _ingredientSelectionService = ingredientsSelectionService;
        }

        [HttpPost]
        public async Task<ActionResult<List<StoreDTO>>> PostIngredients(List<string> ingredients)
        {
            var stores = await _ingredientSelectionService.GetStoresForIngredientsAsync(ingredients);
            return Ok(stores);
        }
    }
}
