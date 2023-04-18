using Domain.Entities.RecipeEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Repository
{
    public interface IRecipeRepository
    {
        Task<GeneratedRecipe> AddGeneratedRecipeAsync(GeneratedRecipe generatedRecipe);
        Task<GeneratedRecipe> GetGeneratedRecipeAsync(int id);
    }
}
