using Domain.Entities.RecipeEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.NutritionsAnalysis
{
    public interface IRecipeIngredientRepository
    {
        Task<RecipeIngredient> GetRecipeIngredientByIdAsync(int id);
        Task<IEnumerable<RecipeIngredient>> GetAllRecipeIngredientsAsync();
        Task AddRecipeIngredientAsync(RecipeIngredient recipeIngredient);
        // Add other necessary methods here
    }


}
