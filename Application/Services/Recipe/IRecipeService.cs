using Application.DTO.RecipeDTOs;

namespace Application.Services.Recipe
{
    public interface IRecipeService
    {
        Task<GeneratedRecipeDTO> GetGeneratedRecipeAsync(RecipeRequestDTO requestDTO);

    }
}
