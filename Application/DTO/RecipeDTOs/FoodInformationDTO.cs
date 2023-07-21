using Newtonsoft.Json;

namespace Application.DTO.RecipeDTOs
{
    public class FoodInformationDTO
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("preparationTime")]
        public string PreparationTime { get; set; }

        [JsonProperty("cookingTime")]
        public string CookingTime { get; set; }

        [JsonProperty("servings")]
        public string Servings { get; set; }

        [JsonProperty("caloriesPerServing")]
        public string? CaloriesPerServing { get; set; }

        [JsonProperty("servingSize")]
        public string ServingSize { get; set; }

        [JsonProperty("dietaryPreferences")]
        public string DietaryPreferences { get; set; }

        [JsonProperty("keyIngredients")]
        public string KeyIngredients { get; set; }

        [JsonProperty("allergyRestrictions")]
        public string AllergyRestrictions { get; set; }

        [JsonProperty("cuisine")]
        public string Cuisine { get; set; }

        [JsonProperty("dishType")]
        public string DishType { get; set; }

        [JsonProperty("cookingMethod")]
        public string CookingMethod { get; set; }
    }
}
