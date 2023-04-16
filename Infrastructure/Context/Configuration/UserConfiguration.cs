using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Setting;
using Domain.Entities.UserEntities;

namespace Infrastructure.Context.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
        {
            public void Configure(EntityTypeBuilder<User> builder)
            {
                builder.ToTable(nameof(User), DatabaseSetting.UserSchema); 
                // Primary key
                builder.HasKey(u => u.UserId);

                // Username
                builder.Property(u => u.Username)
                    .HasMaxLength(50)
                    .IsRequired();

                // Email address
                builder.Property(u => u.EmailAddress)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .IsRequired(); 

                // Relationship to UserProfileInfo
                //builder.HasOne(u => u.UserProfileInfo)
                //    .WithOne()
                //    .HasForeignKey<UserProfileInfo>(upi => upi.UserId)
                //    .OnDelete(DeleteBehavior.Cascade);

                //// Relationship to UserCredentials
                //builder.HasOne(u => u.UserCredentials)
                //    .WithOne()
                //    .HasForeignKey<UserCredentials>(uc => uc.Id)
                //    .OnDelete(DeleteBehavior.Cascade);
                 
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
