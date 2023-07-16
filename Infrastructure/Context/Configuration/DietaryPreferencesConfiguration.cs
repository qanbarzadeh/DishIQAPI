using Domain.Entities.UserEntities;
using Infrastructure.Setting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Context.Configuration
{
    public class DietaryPreferencesConfiguration : Microsoft.EntityFrameworkCore.IEntityTypeConfiguration<DietaryPreferences>
    {
        public void Configure(EntityTypeBuilder<DietaryPreferences> builder)
        {
            builder.ToTable(nameof(DietaryPreferences), DatabaseSetting.UserSchema);

            builder.HasKey(dp => dp.Id);

            builder.Property(dp => dp.Id)
                .HasColumnName("DietaryPreferenceID");

            builder.Property(dp => dp.AllowedDietaryPreferences)
                .HasMaxLength(50)
                .IsRequired();
            //Relationship to User
            builder.HasOne<ApplicationUser>()
                .WithMany(u => u.DietaryPreferences)
                .HasForeignKey(uf => uf.Id)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
