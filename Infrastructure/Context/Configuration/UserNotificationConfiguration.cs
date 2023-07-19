using Domain.Entities.UserEntities;
using Infrastructure.Setting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Context.Configuration
{
    public class UserNotificationConfiguration : IEntityTypeConfiguration<UserNotification>
    {
        public void Configure(EntityTypeBuilder<UserNotification> builder)
        {
            builder.ToTable(nameof(UserNotification), DatabaseSetting.Schema);

            // Primary key
            builder.HasKey(un => un.Id);

            // Set primary key as identity column
            builder.Property(un => un.Id).ValueGeneratedOnAdd();

            // User foreign key
            builder.Property(un => un.UserId).IsRequired();
            /* The relationship with ApplicationUser is removed here */

            // Notification type
            builder.Property(un => un.NotificationType)
                .IsRequired()
                .HasMaxLength(50);

            // Notification text
            builder.Property(un => un.NotificationText)
                .IsRequired()
                .HasMaxLength(500);

            // IsRead
            builder.Property(un => un.IsRead)
                .IsRequired();

            // CreatedAt
            builder.Property(un => un.CreatedAt)
                .IsRequired();
        }
    }
}
