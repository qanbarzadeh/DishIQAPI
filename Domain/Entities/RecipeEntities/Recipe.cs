using Domain.Entities.UserEntities;

namespace Domain.Entities.RecipeEntities
{
    
        public class Recipe
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public string PreparationTime { get; set; } //changed from TimeSpan to string (warning if claculation is required)
            public string  CookingTime { get; set; }
            public int Servings { get; set; }
            public string ServingSize { get; set; }
            public string Cuisine { get; set; }
            public string DishType { get; set; }
            public string CookingMethod { get; set; }
            public double CaloriesPerServing { get; set; }            
            public string UserId { get; set; }
            // Navigation properties
            public ApplicationUser User { get; set; }
            public List<RecipeIngredient> RecipeIngredients { get; set; } = new List<RecipeIngredient>();
        }    
}
