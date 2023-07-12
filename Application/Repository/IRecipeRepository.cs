using Domain.Entities.RecipeEntities;

namespace Application.Repository
{
    public interface IRecipeRepository
    {
        Task<GeneratedRecipe> AddGeneratedRecipeAsync(GeneratedRecipe generatedRecipe);
        Task<GeneratedRecipe> GetGeneratedRecipeAsync(int id);
    }
}
