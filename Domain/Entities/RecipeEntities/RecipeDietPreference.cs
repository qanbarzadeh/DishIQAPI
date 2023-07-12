using Domain.Enums;

namespace Domain.Entities.RecipeEntities
{
    public class RecipeDietPreference
    {
        public int Id { get; set; }
        public DietaryPreferencesEnum DietaryPreferences { get; set; }
    }
}
