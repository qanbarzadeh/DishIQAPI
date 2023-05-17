using Newtonsoft.Json;

namespace Application.DTO.RecipeDTOs
{
    public class IngredientDTO
    {
        [JsonProperty("ingredientInfo")]
        public string IngredientInfo { get; set; }
    }
}
