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
            builder.HasKey(ri => new { ri.RecipeId, ri.IngredientId, ri.UserId });

            // Quantity
            builder.Property(ri => ri.Quantity)
                .IsRequired();

            // Unit
            builder.Property(ri => ri.Unit)
                .HasMaxLength(50)
                .IsRequired();

            // Relationship to Recipe
            builder.HasOne(ri => ri.Recipe)
                .WithMany(r => r.RecipeIngredients)
                .HasForeignKey(ri => ri.RecipeId)
                .OnDelete(DeleteBehavior.Cascade);

            // Relationship to Ingredient
            builder.HasOne(ri => ri.Ingredient)
                .WithMany(i => i.RecipeIngredients)
                .HasForeignKey(ri => ri.IngredientId)
                .OnDelete(DeleteBehavior.Cascade);

            // Relationship to User
            builder.HasOne(ri => ri.User)
                .WithMany(u => u.RecipeIngredients)
                .HasForeignKey(ri => ri.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }

}
