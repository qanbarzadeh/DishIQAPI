using Application.DTO.OpenAiResponse;
using Application.DTO.RecipeDTOs;

namespace Application.Interfaces
{
    public interface IRecipeParser
    {
        FoodInformationDTO ParseFoodInformation(string apiResponse);
        List<IngredientDTO> ParseIngredients(string apiResponse);
        List<CookingStepDTO> ParseCookingSteps(string apiResponse);
    }
}
