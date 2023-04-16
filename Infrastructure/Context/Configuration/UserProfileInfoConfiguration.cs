using Domain.Entities;
using Infrastructure.Setting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Context.Configuration
{
    public class UserProfileInfoConfiguration : IEntityTypeConfiguration<UserProfileInfo>
    {
        public void Configure(EntityTypeBuilder<UserProfileInfo> builder)
        {
            builder.ToTable(nameof(UserProfileInfo), DatabaseSetting.UserSchema); // todo : implement DependencyInjection  and service creation approach 

            builder.HasKey(x => x.UserId); 

            builder.HasOne<User>()
                .WithOne(x => x.UserProfileInfo)
                .HasForeignKey<UserProfileInfo>(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade);
           
            builder.Property(upi => upi.FullName)
                .HasMaxLength(100)
                .IsRequired();


            // Gender
            builder.Property(upi => upi.Gender)
                .IsRequired();

            // Date of birth
            builder.Property(upi => upi.DateOfBirth)
                .IsRequired();

            // Profile picture
            builder.Property(upi => upi.ProfilePicture)
                .HasMaxLength(255);

            // Bio
            builder.Property(upi => upi.Bio)
                .HasMaxLength(500);

            // Location
            builder.Property(upi => upi.Location)
                .HasMaxLength(100);

            // Phone number
            builder.Property(upi => upi.PhoneNumber)
                .HasMaxLength(20);

            // Social media handles
            builder.Property(upi => upi.SocialMediaHandle)
                .HasMaxLength(500);

            // Language preference
            builder.Property(upi => upi.LanguagePreference)
                .HasMaxLength(20);

            // Notification settings
            builder.Property(upi => upi.NotificationSettings)
                .IsRequired();

            // Subscription status
            builder.Property(upi => upi.SubscriptionStatus)
                .HasMaxLength(20);

            // Payment information
            builder.Property(upi => upi.PaymentInformation)
                .HasMaxLength(500);

            // User activity log
            builder.Property(upi => upi.UserActivityLog)
                .HasMaxLength(500);

            // IsSuspicious
            builder.Property(upi => upi.IsSuspicious)
                .IsRequired();

            // IsBlacklisted
            builder.Property(upi => upi.IsBlacklisted)
                .IsRequired();
        }
    }
}
