using Application.DTO.RecipeDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IRecipeInformationService
    {
        Task<FoodInformationDTO> GetFoodInformationAsync(RecipeRequestDTO recipeRequest);
        Task<List<IngredientDTO>> GetIngredientsAsync(RecipeRequestDTO recipeRequest);
        Task<List<CookingStepDTO>> GetCookingStepsAsync(RecipeRequestDTO recipeRequest);
    }
}
