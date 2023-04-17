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
    public class MealTimeConfiguration : IEntityTypeConfiguration<MealTime>
    {
        public void Configure(EntityTypeBuilder<MealTime> builder)
        {
            builder.ToTable(nameof(MealTime));
            builder.HasKey(x => x.Id);

            builder.Property(x => x.MealTimeEnum)
                .IsRequired()
                .HasConversion<int>();

            builder.HasData(Enum.GetValues(typeof(MealTimeEnum))
                .Cast<MealTimeEnum>()
                .Select(e => new MealTime
                {
                    Id = (int)e,
                    MealTimeEnum = e
                }));
        }
    }
}