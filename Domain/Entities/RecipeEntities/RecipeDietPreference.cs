using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.RecipeEntities
{
    public class RecipeDietPreference
    {
        public int Id { get; set; }
        public DietaryPreferencesEnum DietaryPreferences { get; set; } 
    }
}
