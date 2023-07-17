using Domain.Entities.RecipeEntities;
using Domain.Entities.UserEntities;
using Domain.Entities.UserRegistration;
using System.Collections.Generic;

namespace Domain.Entities.Factories.UserRegistration
{
    public static class ApplicationUserFactory
    {
        public static ApplicationUser CreateUser(string username, string email)
        {
            return new ApplicationUser
            {
                Email = email, // Assign the email value to the Email property inherited from IdentityUser
                UserName = username, // Assign the username value to the UserName property inherited from IdentityUser
                Recipes = new List<Recipe>(), // Initialize an empty list of Recipes for the new user
                UserEvents = new List<UserEvent>(), // Initialize an empty list of UserEvents for the new user
                UpdatedAt = DateTime.UtcNow, // Set the UpdatedAt to the current date and time
                IsDeleted = false, // The user is not deleted initially
                Version = 1 // The initial version is 1
            };
        }
    }
}
