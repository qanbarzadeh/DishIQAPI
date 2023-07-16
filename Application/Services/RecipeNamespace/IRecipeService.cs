using Application.DTO.RecipeDTOs;

namespace Application.Services.RecipenameSpace
{
    public interface IRecipeService
    {
        Task<GeneratedRecipeDTO> GetGeneratedRecipeAsync(RecipeRequestDTO requestDTO);

    }
}
