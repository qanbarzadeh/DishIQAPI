using System.ComponentModel.DataAnnotations;

namespace Domain.Entities.RecipeEntities
{
    public class GeneratedRecipe
    {
        [Key]
        public int GeneratedRecipeID { get; set; }

        // Navigation property for RecipeMetadata
        //public RecipeMetadata Metadata { get; set; }

        // Navigation property for FoodInformation
        public int FoodInformationId { get; set; }
        public FoodInformation FoodInformation { get; set; }
        public virtual List<Ingredient> Ingredients { get; set; } = new List<Ingredient>();
        public virtual List<CookingStep> CookingSteps { get; set; } = new List<CookingStep>();
    }
}
