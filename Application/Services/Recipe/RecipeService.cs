using Application.DTO.RecipeDTOs;
using Application.Interfaces;
using Application.Services.OpenAI.ChatGptAPI;

namespace Application.Services.Recipe
{
    public class RecipeService : IRecipeService
    {
        private readonly IChatGptService _chatGptService;
        private readonly IRecipeParser _recipeParser;

        public RecipeService(IChatGptService chatGptService, IRecipeParser recipeParser)
        {
            _chatGptService = chatGptService;
            _recipeParser = recipeParser;
        }

        public async Task<GeneratedRecipeDTO> GetGeneratedRecipeAsync(RecipeRequestDTO requestDTO)
        {
            var apiResponse = await _chatGptService.GeneratedRecipeApiAsync(requestDTO);
            var generatedRecipeDTO = _recipeParser.ParseApiResponse(apiResponse);
            return generatedRecipeDTO;
        }
    }
}
