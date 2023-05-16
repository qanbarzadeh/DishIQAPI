using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO.RecipeDTOs
{
    public class RecipeDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int PreparationTime { get; set; }
        public int CookingTime { get; set; }
        public int Servings { get; set; }
        public int CaloriesPerServing { get; set; }
        public string ServingSize { get; set; }
        public List<string> DietaryPreferences { get; set; }
        public List<string> KeyIngredients { get; set; }
        public List<string> AllergyRestrictions { get; set; }
        public string Cuisine { get; set; }
        public string DishType { get; set; }
        public string CookingMethod { get; set; }
        public List<string> Ingredients { get; set; }
        public List<string> Steps { get; set; }
    }

}
