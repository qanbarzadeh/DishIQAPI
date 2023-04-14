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
                    .WithOne()
                    .HasForeignKey<UserProfileInfo>(upi => upi.UserId)
                    .OnDelete(DeleteBehavior.Cascade);

                // Relationship to UserCredentials
                builder.HasOne(u => u.UserCredentials)
                    .WithOne()
                    .HasForeignKey<UserCredentials>(uc => uc.UserId)
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

                //// Relationship to UserDietaryPreferences                
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

}
