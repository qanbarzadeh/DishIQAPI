using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            builder.HasOne(dp => dp.User)
                .WithMany(u => u.DietaryPreferences)
                .HasForeignKey(dp => dp.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
