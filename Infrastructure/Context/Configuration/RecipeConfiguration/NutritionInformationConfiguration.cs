using Domain.Entities.RecipeEntities;
using Infrastructure.Setting;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

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
            builder.Property(n => n.Calcium).HasPrecision(5, 2);
            builder.Property(n => n.Carbohydrate).HasPrecision(5, 2);
            builder.Property(n => n.Fat).HasPrecision(5, 2);
            builder.Property(n => n.Iron).HasPrecision(5, 2);
            builder.Property(n => n.Protein).HasPrecision(5, 2);
            builder.Property(n => n.Sodium).HasPrecision(5, 2);
            builder.Property(n => n.VitaminA).HasPrecision(5, 2);
            builder.Property(n => n.VitaminC).HasPrecision(5, 2);
            builder.Property(n => n.VitaminD).HasPrecision(5, 2);

            // Relationship to Ingredient
            builder.HasOne(ni => ni.Ingredient)
                .WithOne(i => i.NutritionInformation)
                .HasForeignKey<NutritionInformation>(ni => ni.IngredientId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
