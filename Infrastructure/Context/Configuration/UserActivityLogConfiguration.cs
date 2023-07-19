using Domain.Entities.UserEntities;
using Infrastructure.Setting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Context.Configuration
{
    public class UserActivityLogConfiguration : IEntityTypeConfiguration<UserActivityLog>
    {
        public void Configure(EntityTypeBuilder<UserActivityLog> builder)
        {
            builder.ToTable(nameof(UserActivityLog), DatabaseSetting.Schema);

            builder.HasKey(x => x.Id);

            builder.Property(x => x.ActivityType)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(x => x.ActivityDate)
                .IsRequired();

            builder.Property(x => x.IPAddress)
                .IsRequired()
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnType("varchar(15)");

            builder.Property(x => x.DeviceType)
                .HasMaxLength(50);

            builder.Property(x => x.DeviceOS)
                .HasMaxLength(50);

            builder.Property(x => x.BrowserType)
                .HasMaxLength(50);

            builder.Property(x => x.BrowserVersion)
                .HasMaxLength(50);

            builder.Property(x => x.Location)
                .HasMaxLength(100);

            builder.Property(x => x.Duration)
                .IsRequired(false);
        }
    }
}
