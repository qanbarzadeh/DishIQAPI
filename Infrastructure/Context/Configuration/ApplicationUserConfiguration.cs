using Domain.Entities.UserEntities;
using Infrastructure.Setting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Context.Configuration
{
    public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.ToTable(nameof(ApplicationUser), DatabaseSetting.UserSchema);

            builder.HasKey(u => u.Id);

            builder.Property(u => u.UserName).HasMaxLength(50).IsRequired();

            builder.HasMany(u => u.UserEvents)
                .WithOne(ue => ue.ApplicationUser)
                .HasForeignKey(ue => ue.ApplicationUserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Relationship to Recipes
            builder.HasMany(u => u.Recipes)
                .WithOne(r => r.User)
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }

}
