using Domain.Entities.RecipeEntities;
using Domain.Entities.UserRegistration;
using Microsoft.AspNetCore.Identity;

namespace Domain.Entities.UserEntities
{
    public class ApplicationUser : IdentityUser
    {

        public bool IsDeleted { get; set; } = false;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        public int Version { get; set; } = 1;
        public ICollection<UserEvent> UserEvents { get; set; } = new List<UserEvent>();
        public ICollection<Recipe> Recipes { get; set; } = new List<Recipe>(); 
    }
}
