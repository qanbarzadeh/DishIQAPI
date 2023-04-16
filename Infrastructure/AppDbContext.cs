using Domain.Entities;
using Infrastructure.Context.Configuration;
using Infrastructure.Setting;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public class AppDbContext : DbContext
    {

        public DbSet<User> Users { get; set; }
        public DbSet<UserProfileInfo> UserProfileInfos { get; set; }
        public DbSet<UserCredentials> UserCredentials { get; set; }
        public DbSet<UserAllergy> UserAllergies { get; set; }
        public DbSet<UserCookingSkillLevel> UserCookingSkillLevels { get; set; }
        public DbSet<DietaryPreferences> DietaryPreferences { get; set; }
        public DbSet<UserNotification> UserNotifications { get; set; }
        public DbSet<UserActivityLog> UserActivityLogs { get; set; }
        public DbSet<SocialMediaHandle> SocialMediaHandles { get; set; }
        

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
        }
    }
}

