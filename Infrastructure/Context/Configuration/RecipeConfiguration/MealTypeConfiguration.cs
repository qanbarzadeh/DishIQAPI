using Domain.Entities.RecipeEntities;
using Domain.Enums.RecipeEnums;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Context.Configuration.RecipeConfiguration
{
    public class MealTypeConfiguration : IEntityTypeConfiguration<MealType>
    {
        public void Configure(EntityTypeBuilder<MealType> builder)
        {
            builder.ToTable(nameof(MealType));
            builder.HasKey(x => x.Id);

            builder.Property(x => x.MealName)
                .IsRequired()
                .HasMaxLength(50);

            builder.HasData(Enum.GetValues(typeof(MealTypeEnum))
                  .Cast<MealTypeEnum>()
                  .Select(e => new MealType
                  {
                      Id = (int)e,
                      MealName = e
                  }));
        }
    }
}
