using Application.Services.OpenAI.ChatGptAPI;


namespace Application.Services.Recipe
{
    public class RecipeService : IRecipeService
    {
        private readonly IChatGptService _chatGptService; 

        public RecipeService(IChatGptService chatGptService)
        {
            _chatGptService = chatGptService;
        }
        
        public async Task<GeneratedRecipe> GetGeneratedRecipeAsync(RecipeRequest request)
        {            
            return await _chatGptService.GeneratedRecipeApiAsync(request);
        }
    }
}
