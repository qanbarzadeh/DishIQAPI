using Domain.Entities.UserEntities;
using Infrastructure.Setting;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Context.Configuration
{
    public class BloodTypeConfiguration : IEntityTypeConfiguration<BloodType>
    {
            public void Configure(EntityTypeBuilder<BloodType> builder)
            {
                builder.ToTable("BloodType", DatabaseSetting.RecipeSchema);

                builder.HasKey(e => e.Id);

                builder.Property(e => e.Id)
                    .HasColumnName("Id")
                    .IsRequired();

                builder.Property(e => e.BloodTypeName)
                    .HasColumnName("BloodTypeName")
                    .IsRequired();
            }
        }   
}
