using Domain.Entities.UserEntities;
using Infrastructure.Setting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Context.Configuration
{
    public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.ToTable(nameof(ApplicationUser), DatabaseSetting.UserSchema);

            // Primary key
            builder.HasKey(u => u.UserId);

            // Username
            builder.Property(u => u.Username)
                .HasMaxLength(50)
                .IsRequired();

            // Relationship to Recipes
            builder.HasMany(u => u.Recipes)
                .WithOne(r => r.User)
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Relationship to UserAllergies
            builder.HasMany(u => u.UserAllergies)
                .WithOne()
                .HasForeignKey(ua => ua.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Relationship to UserCookingSkillLevels
            builder.HasOne(u => u.UserCookingSkillLevel)
                .WithOne()
                .HasForeignKey<UserCookingSkillLevel>(us => us.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Relationship to UserDietaryPreferences                
            builder.HasMany(u => u.DietaryPreferences)
                .WithOne()
                .HasForeignKey(dp => dp.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Relationship to UserNotifications
            builder.HasMany(u => u.UserNotifications)
                .WithOne()
                .HasForeignKey(un => un.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Relationship to UserActivityLog
            builder.HasOne(u => u.UserActivityLog)
                .WithOne()
                .HasForeignKey<UserActivityLog>(ual => ual.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
