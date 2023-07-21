using Domain.Entities.UserRegistration;
using Infrastructure.Setting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Context.UserRegsitrationConf
{
    public class AuthUserConfiguration : IEntityTypeConfiguration<AuthUser>
    {
        public void Configure(EntityTypeBuilder<AuthUser> builder)
        {
            builder.ToTable(nameof(AuthUser), DatabaseSetting.Schema);

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd();

            builder.Property(u => u.EmailAddress)
                .HasMaxLength(255)
                .IsRequired();

            builder.Property(u => u.Username)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(u => u.CreatedAt)
                .IsRequired();

            builder.Property(u => u.IsDeleted)
                .HasDefaultValue(false);

            //builder.HasMany(u => u.ExternalLogins)
            //    .WithOne(el => el.AuthUser)
            //    .HasForeignKey(el => el.AuthUserId)
            //    .OnDelete(DeleteBehavior.Cascade);

            //builder.HasMany(u => u.UserEvents)
            //    .WithOne(ue => ue.AuthUser)
            //    .HasForeignKey(ue => ue.AuthUserId)
            //    .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
