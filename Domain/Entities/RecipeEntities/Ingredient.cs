using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
