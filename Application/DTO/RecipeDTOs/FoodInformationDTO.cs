using Newtonsoft.Json;

namespace Application.DTO.RecipeDTOs
{
    public class FoodInformationDTO
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }


        [JsonProperty("PreparationTime")]
        public string PreparationTime { get; set; }

        [JsonProperty("CookingTime")]
        public string CookingTime { get; set; }

        [JsonProperty("Servings ")]
        public string Servings { get; set; }

        [JsonProperty("CaloriesPerServing")]
        public string CaloriesPerServing { get; set; }

        [JsonProperty("ServingSize")]
        public string ServingSize { get; set; }

        [JsonProperty("DietaryPreferences")]
        public string DietaryPreferences { get; set; }

        [JsonProperty("KeyIngredients")]
        public string KeyIngredients { get; set; }

        [JsonProperty("AllergyRestrictions")]
        public string AllergyRestrictions { get; set; }

        [JsonProperty("Cuisine")]
        public string Cuisine { get; set; }

        [JsonProperty("DishType")]
        public string DishType { get; set; }

        [JsonProperty("CookingMethod")]
        public string CookingMethod { get; set; }
    }
}
