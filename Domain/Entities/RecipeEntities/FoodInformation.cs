using System.ComponentModel.DataAnnotations;

namespace Domain.Entities.RecipeEntities
{
    public class FoodInformation
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public TimeSpan PreparationTime { get; set; }
        public int CookingTime { get; set; }
        public int Servings { get; set; }
        public int CaloriesPerServing { get; set; }
        public int ServingSize { get; set; }
        public string DietaryPreferences { get; set; }
        public string KeyIngredients { get; set; }
        public string AllergyRestrictions { get; set; }
        public string Cuisine { get; set; }
        public string DishType { get; set; }
        public string CookingMethod { get; set; }
    }
}
