using Domain.Entities.RecipeEntities;
using Microsoft.AspNetCore.Identity;

namespace Domain.Entities.UserEntities
{
    public class ApplicationUser : IdentityUser
    {
        public string UserId { get; set; } // Updated to string

        //public int UserId { get; set; }
        public string Username { get; set; }

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
