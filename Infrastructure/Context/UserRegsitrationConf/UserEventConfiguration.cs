using Domain.Entities.UserRegistration;
using Infrastructure.Setting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Context.UserRegsitrationConf
{
    public class UserEventConfiguration : IEntityTypeConfiguration<UserEvent>
    {
        public void Configure(EntityTypeBuilder<UserEvent> builder)
        {
            builder.ToTable(nameof(UserEvent), DatabaseSetting.AuthenticationSchema);

            builder.HasKey(x => x.Id);

            builder.Property(ue => ue.AuthUserId)
                .IsRequired();

            builder.Property(ue => ue.EventType)
                .IsRequired();

            builder.Property(ue => ue.EventDate)
                .IsRequired();

            //builder.HasOne<AuthUser>()
            //    .WithMany(u => u.UserEvents)
            //    .HasForeignKey(ue => ue.AuthUserId)
            //    .OnDelete(DeleteBehavior.Cascade);
        }
    }
}