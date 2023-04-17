using Domain.Entities.RecipeEntities;
using Domain.Entities.UserEntities;
using Infrastructure.Context.Configuration;
using Infrastructure.Context.Configuration.RecipeConfiguration;
using Infrastructure.Setting;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public class AppDbContext : DbContext
    {
        // DbSets for User entities
        public DbSet<User> Users { get; set; }
        public DbSet<UserProfileInfo> UserProfileInfos { get; set; }
        public DbSet<UserCredentials> UserCredentials { get; set; }
        public DbSet<UserAllergy> UserAllergies { get; set; }
        public DbSet<UserCookingSkillLevel> UserCookingSkillLevels { get; set; }
        public DbSet<DietaryPreferences> DietaryPreferences { get; set; }
        public DbSet<UserNotification> UserNotifications { get; set; }
        public DbSet<UserActivityLog> UserActivityLogs { get; set; }
        public DbSet<SocialMediaHandle> SocialMediaHandles { get; set; }

        // DbSets for Recipe entities
        public DbSet<RecipeDietPreference> RecipeDietPrefernce { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<CookingStep> CookingStep { get; set; }
        public DbSet<MealType> MealTypes { get; set; }        
        public DbSet<Region> Regions { get; set; }
        public DbSet<CookingTechnique> CookingTechniques { get; set; }
        public DbSet<Flavor> Flavors { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<MealTime> MealTimes { get; set; }
        public DbSet<Dislike> Dislikes { get; set; }
        public DbSet<BloodType> BloodTypes { get; set; }


        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.ApplyConfigurationsFromAssembly(typeof(UserConfiguration).Assembly);           
            modelBuilder.HasDefaultSchema(DatabaseSetting.UserSchema);
            // Apply configurations
            new UserConfiguration().Configure(modelBuilder.Entity<User>());
            new UserProfileInfoConfiguration().Configure(modelBuilder.Entity<UserProfileInfo>());
            new UserCredentialsConfiguration().Configure(modelBuilder.Entity<UserCredentials>());
            new UserAllergyConfiguration().Configure(modelBuilder.Entity<UserAllergy>());
            new UserCookingSkillLevelConfiguration().Configure(modelBuilder.Entity<UserCookingSkillLevel>());
            new DietaryPreferencesConfiguration().Configure(modelBuilder.Entity<DietaryPreferences>());
            new UserNotificationConfiguration().Configure(modelBuilder.Entity<UserNotification>());
            new UserActivityLogConfiguration().Configure(modelBuilder.Entity<UserActivityLog>());
            new SocialHandleConfiguration().Configure(modelBuilder.Entity<SocialMediaHandle>());

            // Apply configurations for Recipe related entities
            new MealTypeConfiguration().Configure(modelBuilder.Entity<MealType>());            
            new RegionConfiguration().Configure(modelBuilder.Entity<Region>());
            new CookingTechniqueConfiguration().Configure(modelBuilder.Entity<CookingTechnique>());
            new FlavorConfiguration().Configure(modelBuilder.Entity<Flavor>());
            new CountryConfiguration().Configure(modelBuilder.Entity<Country>());
            new MealTimeConfiguration().Configure(modelBuilder.Entity<MealTime>());
            new DislikeConfiguration().Configure(modelBuilder.Entity<Dislike>());
            new CookingStepConfiguration().Configure(modelBuilder.Entity<CookingStep>());
            new RecipeDietPreferenceConfiguration().Configure(modelBuilder.Entity<RecipeDietPreference>());
        }
    }
}

