using Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class DietaryPreferences
    {
        [Key]
        public int Id { get; set; }

        //[Required]
        //[ForeignKey("User")]
        public int UserId { get; set; }
        [Required]
        public DietaryPreferencesEnum AllowedDietaryPreferences { get; set; }

        public DietaryPreferences() { } // Empty constructor needed for EF Core

        // Navigation property to User  
        public virtual User User { get; set; }
    }
}
