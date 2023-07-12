namespace Domain.Entities.RecipeEntities
{
    public class Ingredient
    {
        public int Id { get; set; }
        public string Name { get; set; }
        // Navigation properties
        public NutritionInformation NutritionInformation { get; set; }
        public List<RecipeIngredient> RecipeIngredients { get; set; }
    }

}
