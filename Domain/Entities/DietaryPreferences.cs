using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Enums;

namespace Domain.Entities
{
    public class UserDietaryPreferences
    {
        //[Key] Configuratuion move to Fluenbt API 
        public int Id { get; set; }

        [Required]
        //[ForeignKey("User")] confoguration move to Fluent API 
        public int UserId { get; set; }
        [Required]
        public DietaryPreferencesEnum AllowedDietaryPreferences { get; set; } = DietaryPreferencesEnum.Vegan | DietaryPreferencesEnum.Vegetarian;

        public UserDietaryPreferences() { } // Empty constructor needed for EF Core

        // Navigation property to User
        public virtual User? User { get; set; }
    }
}
