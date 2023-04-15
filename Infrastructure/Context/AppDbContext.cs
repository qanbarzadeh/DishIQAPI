using Domain.Entities;
using Infrastructure.Context.Configuration;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Context
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
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(UserConfiguration).Assembly);            
        }
    }
}

