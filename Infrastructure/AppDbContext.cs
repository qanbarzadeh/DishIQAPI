using Domain.Entities.RecipeEntities;
using Domain.Entities.UserEntities;
using Infrastructure.Context.Configuration;
using Infrastructure.Context.Configuration.RecipeConfiguration;
using Infrastructure.Setting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        // DbSets for User entities
        public DbSet<ApplicationUser> ApplicationUser { get; set; }                        
        public DbSet<DietaryPreferences> DietaryPreferences { get; set; }
        public DbSet<UserNotification> UserNotifications { get; set; }
        public DbSet<UserActivityLog> UserActivityLogs { get; set; }
        public DbSet<SocialMediaHandle> SocialMediaHandles { get; set; }

        // DbSets for Recipe entities
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<RecipeIngredient> RecipeIngredients { get; set; }
        public DbSet<NutritionInformation> NutritionInformations { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); // Very important!

            modelBuilder.HasDefaultSchema(DatabaseSetting.UserSchema);

            // Apply configurations for User related entities
            new ApplicationUserConfiguration().Configure(modelBuilder.Entity<ApplicationUser>());                                    
            new DietaryPreferencesConfiguration().Configure(modelBuilder.Entity<DietaryPreferences>());
            new UserNotificationConfiguration().Configure(modelBuilder.Entity<UserNotification>());
            new UserActivityLogConfiguration().Configure(modelBuilder.Entity<UserActivityLog>());
            new SocialHandleConfiguration().Configure(modelBuilder.Entity<SocialMediaHandle>());

            // Change default schema for Recipe related entities
            modelBuilder.HasDefaultSchema(DatabaseSetting.RecipeSchema);

            // Apply configurations for Recipe related entities
            new RecipeConfiguration().Configure(modelBuilder.Entity<Recipe>());
            new IngredientConfiguration().Configure(modelBuilder.Entity<Ingredient>());
            new RecipeIngredientConfiguration().Configure(modelBuilder.Entity<RecipeIngredient>());
            new NutritionInformationConfiguration().Configure(modelBuilder.Entity<NutritionInformation>());
        }
    }
}
