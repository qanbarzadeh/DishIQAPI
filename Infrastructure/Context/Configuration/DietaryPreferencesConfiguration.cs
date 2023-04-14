using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace Infrastructure.Context.Configuration
{
    public class DietaryPreferencesConfiguration : IEntityTypeConfiguration<DietaryPreferences>
    {
        public void Configure(EntityTypeBuilder<DietaryPreferences> builder)
        {
            builder.ToTable("DietaryPreferences", "User");
            
            builder.HasKey(dp => dp.Id);

            builder.Property(dp => dp.Id)
                .HasColumnName("DietaryPreferenceID");

            builder.Property(dp => dp.AllowedDietaryPreferences)
                .HasMaxLength(50)
                .IsRequired();

        }
    }
}
