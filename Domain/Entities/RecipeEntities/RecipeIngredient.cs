using Domain.Entities.UserEntities;

namespace Domain.Entities.RecipeEntities
{

    public class RecipeIngredient
    {
        public int RecipeId { get; set; }
        public Domain.Entities.RecipeEntities.Recipe Recipe { get; set; }
        public int IngredientId { get; set; }
        public Ingredient Ingredient { get; set; }
        public decimal Quantity { get; set; }
        public string Unit { get; set; }

        // User ID from User entity
        public int UserId { get; set; }

        // Navigation property to User
        public ApplicationUser  ApplicationUser { get; set; }
    }
}