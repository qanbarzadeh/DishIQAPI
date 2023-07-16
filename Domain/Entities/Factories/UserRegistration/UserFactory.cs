using Domain.Entities.RecipeEntities;
using Domain.Entities.UserEntities;
using Domain.Entities.UserRegistration;

namespace Domain.Entities.Factories.UserRegistration
{
    public static class UserFactory
    {
        public static ApplicationUser CreateUser(string username, string email, string userId)
        {
            return new ApplicationUser
            {
                UserId = userId,
                Username = username,                
                // Navigation properties to related entities
                Recipes = new List<Recipe>(),
                RecipeIngredients = new List<RecipeIngredient>(),
                UserProfileInfo = new UserProfileInfo(),
                UserAllergies = new List<UserAllergy>(),
                UserCookingSkillLevel = new UserCookingSkillLevel(),
                DietaryPreferences = new List<DietaryPreferences>(),
                UserNotifications = new List<UserNotification>(),
                UserActivityLog = new UserActivityLog()
            };
        }
    }
}
