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
    public class UserCredentialsConfiguration : IEntityTypeConfiguration<UserCredentials>
    {
        public void Configure(EntityTypeBuilder<UserCredentials> builder)
        {
            builder.ToTable(nameof(UserCredentials), DatabaseSetting.UserSchema);
            builder.HasKey(uc => uc.UserId);

            // Username
            builder.Property(uc => uc.Username)
                .HasMaxLength(50)
                .IsRequired();

            // Email address
            builder.Property(uc => uc.EmailAddress)
                .HasMaxLength(100)
                .IsRequired();

            // Password
            builder.Property(uc => uc.Password)
                .HasMaxLength(255)
                .IsRequired();

            // Account status
            builder.Property(uc => uc.AccountStatus)
                .IsRequired();

            // Last login date time
            builder.Property(uc => uc.LastLoginDateTime)
                .IsRequired();

            // Account creation date time
            builder.Property(uc => uc.AccountCreationDateTime)
                .IsRequired();

            // Password reset token
            builder.Property(uc => uc.PasswordResetToken)
                .HasMaxLength(255);

            // Password reset expiration date time
            builder.Property(uc => uc.PasswordResetExpirationDateTime);

            // Relationship to User
            builder.HasOne<User>()
                .WithOne(u => u.UserCredentials)
                .HasForeignKey<UserCredentials>(uc => uc.UserId)
                .OnDelete(DeleteBehavior.Cascade); 
        }
    }
}
