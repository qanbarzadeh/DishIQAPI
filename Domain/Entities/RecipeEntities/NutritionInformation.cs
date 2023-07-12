using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.RecipeEntities
{
    public class NutritionInformation
    {
        public int Id { get; set; }
        public int IngredientId { get; set; }

        // Navigation property
        public Ingredient Ingredient { get; set; }

        public decimal Carbohydrate { get; set; }
        public decimal Protein { get; set; }
        public decimal Fat { get; set; }
        public decimal VitaminA { get; set; }
        public decimal VitaminC { get; set; }
        public decimal VitaminD { get; set; }
        public decimal Calcium { get; set; }
        public decimal Iron { get; set; }
        public decimal Sodium { get; set; }
    }
}
