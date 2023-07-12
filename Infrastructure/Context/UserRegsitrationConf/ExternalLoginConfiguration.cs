using Domain.Entities.UserRegistration;
using Infrastructure.Setting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Context.UserRegsitrationConf
{
    public class ExternalLoginConfiguration : IEntityTypeConfiguration<ExternalLogin>
    {
        public void Configure(EntityTypeBuilder<ExternalLogin> builder)
        {
            builder.ToTable(nameof(ExternalLogin), DatabaseSetting.AuthenticationSchema);

            builder.HasKey(x => x.Id);

            builder.Property(el => el.LoginProvider)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(el => el.ProviderKey)
                .HasMaxLength(255)
                .IsRequired();

            //builder.HasOne<AuthUser>()
            //    .WithMany(u => u.ExternalLogins)
            //    .HasForeignKey(el => el.AuthUserId)
            //    .OnDelete(DeleteBehavior.Cascade);

            builder.Property(el => el.LinkedAt)
                .IsRequired();

            builder.Property(el => el.IsDeleted)
                .HasDefaultValue(false);
        }
    }
}
