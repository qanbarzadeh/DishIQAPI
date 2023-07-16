using Domain.Entities.RecipeEntities;
using Domain.Entities.UserEntities;
using Domain.Entities.UserRegistration;
using System.Collections.Generic;

namespace Domain.Entities.Factories.UserRegistration
{
    public static class UserFactory
    {
        public static ApplicationUser CreateUser(string username, string email)
        {
            return new ApplicationUser
            {
                Email = email, // Assign the email value to the Email property inherited from IdentityUser
                UserName = username, // Assign the username value to the UserName property inherited from IdentityUser
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
