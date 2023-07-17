using Domain.Entities.RecipeEntities;
using Infrastructure.Setting;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Context.Configuration.RecipeConfiguration
{
    public class RecipeIngredientConfiguration : IEntityTypeConfiguration<RecipeIngredient>
    {
        public void Configure(EntityTypeBuilder<RecipeIngredient> builder)
        {
            builder.ToTable(nameof(RecipeIngredient), DatabaseSetting.RecipeSchema);

            // RecipeId and IngredientId composite key
            builder.HasKey(ri => new { ri.RecipeId, ri.IngredientId });

            // Quantity
            builder.Property(ri => ri.Quantity)
                .IsRequired()
                .HasPrecision(5, 2);  // Add this line to set the precision and scale for the Quantity field

            // Unit
            builder.Property(ri => ri.Unit)
                .HasMaxLength(50)
                .IsRequired();

            // Relationship to Recipe
            builder.HasOne(ri => ri.Recipe)
                .WithMany(r => r.RecipeIngredients)
                .HasForeignKey(ri => ri.RecipeId)
                .OnDelete(DeleteBehavior.Restrict); // Changed from Cascade to Restrict

            // Relationship to Ingredient
            builder.HasOne(ri => ri.Ingredient)
                .WithMany(i => i.RecipeIngredients)
                .HasForeignKey(ri => ri.IngredientId)
                .OnDelete(DeleteBehavior.Restrict); // Changed from Cascade to Restrict
        }
    }
}
