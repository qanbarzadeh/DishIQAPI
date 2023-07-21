using Domain.Enums;

namespace Domain.Entities.UserEntities
{
    public class DietaryPreferences
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public DietaryPreferencesEnum AllowedDietaryPreferences { get; set; }
        public DietaryPreferences() { } // Empty constructor needed for EF Core        
    }
}
