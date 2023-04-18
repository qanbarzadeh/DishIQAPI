using Domain.Entities.RecipeEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Recipe
{
    public interface IRecipeService
    {
        Task<GeneratedRecipe> GetGeneratedRecipeAsync(RecipeRequest request);

    }
}
