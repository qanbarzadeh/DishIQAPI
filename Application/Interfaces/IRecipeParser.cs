using Application.DTO.OpenAiResponse;
using Application.DTO.RecipeDTOs;

namespace Application.Interfaces
{
    public interface IRecipeParser
    {
        GeneratedRecipeDTO ParseApiResponse(ApiResponseDTO apiResponse);
        FoodInformationDTO ParseFoodInformationFromContent(string apiResponse);
        List<IngredientDTO> ParseIngredients(string apiResponse);
        List<CookingStepDTO> ParseCookingSteps(string apiResponse);
    }
}
