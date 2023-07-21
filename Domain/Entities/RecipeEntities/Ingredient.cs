namespace Domain.Entities.RecipeEntities
{
    public class Ingredient
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string DietaryPreferences { get; set; } // New field
        public string AllergyRestrictions { get; set; } // New field

        // Navigation properties
        public NutritionInformation NutritionInformation { get; set; }
        public List<RecipeIngredient> RecipeIngredients { get; set; }
    }

}
