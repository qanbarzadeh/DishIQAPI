using Application.DTO.RecipeDTOs;
using Application.Interfaces;
using Application.Services.OpenAI.ChatGptAPI;

public class RecipeInformationService : IRecipeInformationService
{
    private readonly IChatGptService _chatGptService;
    private readonly IRecipeParser _recipeParser;
    private GeneratedRecipeDTO _generatedRecipe; // Cache the generated recipe

    public RecipeInformationService(IChatGptService chatGptService, IRecipeParser recipeParser)
    {
        _chatGptService = chatGptService;
        _recipeParser = recipeParser;
    }
    private async Task EnsureRecipeGeneratedAsync(RecipeRequestDTO recipeRequest)
    {
        if (_generatedRecipe == null)
        {
            var apiResponse = await _chatGptService.GeneratedRecipeApiAsync(recipeRequest);
            var messageContent = apiResponse.Choices.FirstOrDefault()?.Message?.Content;


            if (string.IsNullOrEmpty(messageContent))
            {
                throw new Exception("Invalid API response: Message content is empty.");
            }

            var foodInformation = _recipeParser.ParseFoodInformation(messageContent);
            var ingredients = _recipeParser.ParseIngredients(messageContent);
            var cookingSteps = _recipeParser.ParseCookingSteps(messageContent);

            _generatedRecipe = new GeneratedRecipeDTO
            {
                FoodInformation = foodInformation,
                Ingredients = ingredients,
                CookingSteps = cookingSteps
            };
        }
    }

    public async Task<FoodInformationDTO> GetFoodInformationAsync(RecipeRequestDTO recipeRequest)
    {
        await EnsureRecipeGeneratedAsync(recipeRequest);
        return _generatedRecipe.FoodInformation;
    }

    public async Task<List<IngredientDTO>> GetIngredientsAsync(RecipeRequestDTO recipeRequest)
    {
        await EnsureRecipeGeneratedAsync(recipeRequest);
        return _generatedRecipe.Ingredients;
    }

    public async Task<List<CookingStepDTO>> GetCookingStepsAsync(RecipeRequestDTO recipeRequest)
    {
        await EnsureRecipeGeneratedAsync(recipeRequest);
        return _generatedRecipe.CookingSteps;
    }
}
