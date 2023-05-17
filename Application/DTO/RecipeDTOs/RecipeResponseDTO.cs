using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO.RecipeDTOs
{
    public class RecipeResponseDTO
    {
        [JsonProperty("foodInformation")]
        public FoodInformationDTO FoodInformation { get; set; }
        [JsonProperty("ingredients")]
        public List<IngredientDTO> Ingredients { get; set; }
        [JsonProperty("cookingSteps")]

        public List<CookingStepDTO> CookingSteps { get; set; }
    }
}
