using Domain.Entities.RecipeEntities;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Domain.Entities.UserEntities
{
    public class ApplicationUser : IdentityUser
    {
        // Removed the UserId and Username properties, as they're inherited from IdentityUser

        // Navigation properties to related entities
        public ICollection<Recipe> Recipes { get; set; } = new List<Recipe>(); // New property
        public ICollection<RecipeIngredient> RecipeIngredients { get; set; } = new List<RecipeIngredient>();
        public virtual UserProfileInfo UserProfileInfo { get; set; } = new UserProfileInfo();
        public virtual ICollection<UserAllergy> UserAllergies { get; set; } = new List<UserAllergy>();
        public virtual UserCookingSkillLevel UserCookingSkillLevel { get; set; } = new UserCookingSkillLevel();
        public virtual ICollection<DietaryPreferences> DietaryPreferences { get; set; } = new List<DietaryPreferences>();
        public virtual ICollection<UserNotification> UserNotifications { get; set; } = new List<UserNotification>();
        public virtual UserActivityLog UserActivityLog { get; set; } = new UserActivityLog();
    }
}
