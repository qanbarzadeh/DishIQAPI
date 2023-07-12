using Domain.Entities.UserEntities;
using Infrastructure.Context.Configuration;
using Infrastructure.Setting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public class AppDbContext : IdentityDbContext<IdentityUser>
    {
        // DbSets for User entities
        // Remove User and UserCredentials if you use IdentityUser
        public DbSet<UserProfileInfo> UserProfileInfos { get; set; }
        public DbSet<UserAllergy> UserAllergies { get; set; }
        public DbSet<UserCookingSkillLevel> UserCookingSkillLevels { get; set; }
        public DbSet<DietaryPreferences> DietaryPreferences { get; set; }
        public DbSet<UserNotification> UserNotifications { get; set; }
        public DbSet<UserActivityLog> UserActivityLogs { get; set; }
        public DbSet<SocialMediaHandle> SocialMediaHandles { get; set; }

        // DbSets for Recipe entities
        

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); // Very important!

            //modelBuilder.ApplyConfigurationsFromAssembly(typeof(UserConfiguration).Assembly);           
            modelBuilder.HasDefaultSchema(DatabaseSetting.UserSchema);

            // Apply configurations
            // Skip UserConfiguration and UserCredentialsConfiguration if you use IdentityUser
            new UserProfileInfoConfiguration().Configure(modelBuilder.Entity<UserProfileInfo>());
            new UserAllergyConfiguration().Configure(modelBuilder.Entity<UserAllergy>());
            new UserCookingSkillLevelConfiguration().Configure(modelBuilder.Entity<UserCookingSkillLevel>());
            new DietaryPreferencesConfiguration().Configure(modelBuilder.Entity<DietaryPreferences>());
            new UserNotificationConfiguration().Configure(modelBuilder.Entity<UserNotification>());
            new UserActivityLogConfiguration().Configure(modelBuilder.Entity<UserActivityLog>());
            new SocialHandleConfiguration().Configure(modelBuilder.Entity<SocialMediaHandle>());

            // Apply configurations for Recipe related entities
            
        }
    }
}
