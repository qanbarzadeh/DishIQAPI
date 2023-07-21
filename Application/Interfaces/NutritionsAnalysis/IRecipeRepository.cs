using Domain.Entities.RecipeEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.NutritionsAnalysis
{
    public interface IRecipeRepository
    {
        Task<Recipe> GetRecipeByIdAsync(int id);    
        Task<IEnumerable<Recipe>> GetAllRecipesAsync();
        Task AddRecipeAsync(Recipe recipe);
        // Add other necessary methods here
    }

}
