using Domain.Entities.UserEntities;
using Infrastructure.Setting;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

public class DietaryPreferencesConfiguration : IEntityTypeConfiguration<DietaryPreferences>
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
    }
}
