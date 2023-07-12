using Domain.Entities.RecipeEntities;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities.UserEntities
{
    public class User
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        [EmailAddress]

        // Navigation properties to related entities
        public ICollection<Recipe> Recipes { get; set; } = new List<Recipe>(); // New property
        public virtual UserProfileInfo UserProfileInfo { get; set; } = new UserProfileInfo();
        public virtual ICollection<UserAllergy> UserAllergies { get; set; } = new List<UserAllergy>();
        public virtual UserCookingSkillLevel UserCookingSkillLevel { get; set; } = new UserCookingSkillLevel();
        public virtual ICollection<DietaryPreferences> DietaryPreferences { get; set; } = new List<DietaryPreferences>();
        public virtual ICollection<UserNotification> UserNotifications { get; set; } = new List<UserNotification>();
        public virtual UserActivityLog UserActivityLog { get; set; } = new UserActivityLog();
    }
}
