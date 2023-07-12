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
    public class IngredientConfiguration : IEntityTypeConfiguration<Ingredient>
    {
        public void Configure(EntityTypeBuilder<Ingredient> builder)
        {
            builder.ToTable(nameof(Ingredient), DatabaseSetting.RecipeSchema);

            // Id
            builder.HasKey(i => i.Id);

            // Name
            builder.Property(i => i.Name)
                .HasMaxLength(100)
                .IsRequired();

            // Relationship to NutritionInformation
            builder.HasOne(i => i.NutritionInformation)
                .WithOne(n => n.Ingredient)
                .HasForeignKey<NutritionInformation>(n => n.IngredientId)
                .OnDelete(DeleteBehavior.Cascade);

            // Relationship to RecipeIngredients
            builder.HasMany(i => i.RecipeIngredients)
                .WithOne(ri => ri.Ingredient)
                .HasForeignKey(ri => ri.IngredientId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
