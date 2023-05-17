using Newtonsoft.Json;

namespace Application.DTO.RecipeDTOs
{
    public class GeneratedRecipeDTO
    {
        [JsonProperty("foodInformation")]
        public FoodInformationDTO FoodInformation { get; set; } = new FoodInformationDTO();

        [JsonProperty("ingredients")]
        public List<IngredientDTO> Ingredients { get; set; } = new List<IngredientDTO>();

        [JsonProperty("cookingSteps")]
        public List<CookingStepDTO> CookingSteps { get; set; } = new List<CookingStepDTO>();
    }
}
