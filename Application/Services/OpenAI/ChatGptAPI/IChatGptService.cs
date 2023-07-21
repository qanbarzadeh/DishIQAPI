using Application.DTO.OpenAiResponse;
using Application.DTO.RecipeDTOs;

namespace Application.Services.OpenAI.ChatGptAPI
{
    public interface IChatGptService
    {
        Task<ApiResponseDTO> GeneratedRecipeApiAsync(RecipeRequestDTO recipeRequestDTO);
    }
}

