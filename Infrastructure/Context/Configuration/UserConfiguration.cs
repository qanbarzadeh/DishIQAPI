using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Context.Configuration
{


    namespace Infrastructure.Data.Configurations
    {
        public class UserConfiguration : IEntityTypeConfiguration<User>
        {
            public void Configure(EntityTypeBuilder<User> builder)
            {
                // Primary key
                builder.HasKey(u => u.UserId);

                // Username
                builder.Property(u => u.Username)
                    .HasMaxLength(50)
                    .IsRequired();

                // Email address
                builder.Property(u => u.EmailAddress)
                    .HasMaxLength(100)
                    .IsRequired();

                // Relationship to UserProfileInfo
                builder.HasOne(u => u.UserProfileInfo)
                    .WithOne(upi => upi.User)
                    .HasForeignKey<UserProfileInfo>(upi => upi.UserId)
                    .OnDelete(DeleteBehavior.Cascade);

                // Relationship to UserCredentials
                builder.HasOne(u => u.UserCredentials)
                    .WithOne(uc => uc.User)
                    .HasForeignKey<UserCredentials>(uc => uc.UserId)
                    .OnDelete(DeleteBehavior.Cascade);

                // Relationship to UserAllergies
                builder.HasMany(u => u.UserAllergies)
                    .WithOne(ua => ua.User)
                    .HasForeignKey(ua => ua.UserId)
                    .OnDelete(DeleteBehavior.Cascade);

                // Relationship to UserCookingSkillLevels
                builder.HasMany(u => u.UserCookingSkillLevels)
                    .WithOne(us => us.User)
                    .HasForeignKey(us => us.UserId)
                    .OnDelete(DeleteBehavior.Cascade);

                //// Relationship to UserDietaryPreferences
                // Relationship to DietaryPreferences
                builder.HasOne(u => u.DietaryPreferences)
                       .WithOne(dp => dp.User)
                       .HasForeignKey<DietaryPreferences>(dp => dp.UserId)
                       .OnDelete(DeleteBehavior.Cascade);


                // Relationship to UserNotifications
                builder.HasMany(u => u.UserNotifications)
                    .WithOne(un => un.User)
                    .HasForeignKey(un => un.UserId)
                    .OnDelete(DeleteBehavior.Cascade);

                // Relationship to UserActivityLog
                builder.HasOne(u => u.UserActivityLog)
                    .WithOne(ual => ual.User)
                    .HasForeignKey<UserActivityLog>(ual => ual.UserId)
                    .OnDelete(DeleteBehavior.Cascade);

            }
        }
    }

}
