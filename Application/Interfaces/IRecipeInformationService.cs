using Application.DTO.RecipeDTOs;

namespace Application.Interfaces
{
    public interface IRecipeInformationService
    {
        Task<FoodInformationDTO> GetFoodInformationAsync(RecipeRequestDTO recipeRequest);
        Task<List<IngredientDTO>> GetIngredientsAsync(RecipeRequestDTO recipeRequest);
        Task<List<CookingStepDTO>> GetCookingStepsAsync(RecipeRequestDTO recipeRequest);
    }
}
