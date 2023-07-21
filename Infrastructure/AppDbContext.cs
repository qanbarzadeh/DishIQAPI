using Domain.Entities.RecipeEntities;
using Domain.Entities.UserEntities;
using Infrastructure.Context.Configuration.RecipeConfiguration;
using Infrastructure.Context.Configuration;
using Infrastructure.Setting;
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
        public DbSet<NutritionInformation> NutritionInformation { get; set; }

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
            // Change default schema for all entities
            modelBuilder.HasDefaultSchema(DatabaseSetting.Schema);

            base.OnModelCreating(modelBuilder); // Now this is after setting the default schema

            // Apply configurations for all entities
            new ApplicationUserConfiguration().Configure(modelBuilder.Entity<ApplicationUser>());
            new DietaryPreferencesConfiguration().Configure(modelBuilder.Entity<DietaryPreferences>());
            new UserNotificationConfiguration().Configure(modelBuilder.Entity<UserNotification>());
            new UserActivityLogConfiguration().Configure(modelBuilder.Entity<UserActivityLog>());
            new SocialHandleConfiguration().Configure(modelBuilder.Entity<SocialMediaHandle>());
            new NutritionInformationConfiguration().Configure(modelBuilder.Entity<NutritionInformation>());

            new RecipeConfiguration().Configure(modelBuilder.Entity<Recipe>());
            new IngredientConfiguration().Configure(modelBuilder.Entity<Ingredient>());
            new RecipeIngredientConfiguration().Configure(modelBuilder.Entity<RecipeIngredient>());
            new NutritionInformationConfiguration().Configure(modelBuilder.Entity<NutritionInformation>());
        }
    }
}
