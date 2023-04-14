using Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class DietaryPreferences
    {
        public int Id { get; set; }   
        public int UserId { get; set; }        
        public DietaryPreferencesEnum AllowedDietaryPreferences { get; set; }
        public DietaryPreferences() { } // Empty constructor needed for EF Core        
    }
}
