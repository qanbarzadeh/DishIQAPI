using Domain.Entities.RecipeEntities;
using Infrastructure.Setting;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Context.Configuration.RecipeConfiguration
{
    public class NutritionInformationConfiguration : IEntityTypeConfiguration<NutritionInformation>
    {
        public void Configure(EntityTypeBuilder<NutritionInformation> builder)
        {
            builder.ToTable(nameof(NutritionInformation), DatabaseSetting.RecipeSchema);

            // Primary key
            builder.HasKey(ni => ni.Id);

            // Nutrient properties
            builder.Property(ni => ni.Carbohydrate).IsRequired();
            builder.Property(ni => ni.Protein).IsRequired();
            builder.Property(ni => ni.Fat).IsRequired();
            builder.Property(ni => ni.VitaminA).IsRequired();
            builder.Property(ni => ni.VitaminC).IsRequired();
            builder.Property(ni => ni.VitaminD).IsRequired();
            builder.Property(ni => ni.Calcium).IsRequired();
            builder.Property(ni => ni.Iron).IsRequired();
            builder.Property(ni => ni.Sodium).IsRequired();

            // Relationship to Ingredient
            builder.HasOne(ni => ni.Ingredient)
                .WithOne(i => i.NutritionInformation)
                .HasForeignKey<NutritionInformation>(ni => ni.IngredientId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
