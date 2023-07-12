using Domain.Entities.RecipeEntities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Context.Configuration.RecipeConfiguration
{
    public class RecipeConfiguration : IEntityTypeConfiguration<Recipe>
    {
        public void Configure(EntityTypeBuilder<Recipe> builder)
        {
            builder.ToTable("Recipes");

            // Primary key
            builder.HasKey(r => r.Id);

            // Properties
            builder.Property(r => r.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(r => r.Description)
                .IsRequired();

            builder.Property(r => r.PreparationTime)
                .IsRequired();

            builder.Property(r => r.CookingTime)
                .IsRequired();

            builder.Property(r => r.Servings)
                .IsRequired();

            builder.Property(r => r.Cuisine)
                .HasMaxLength(50);

            builder.Property(r => r.DishType)
                .HasMaxLength(50);

            builder.Property(r => r.CookingMethod)
                .HasMaxLength(50);

            builder.Property(r => r.CaloriesPerServing)
                .IsRequired();

            // User relationship
            builder.HasOne(r => r.User)
                .WithMany(u => u.Recipes)
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // RecipeIngredient relationship
            builder.HasMany(r => r.RecipeIngredients)
                .WithOne(ri => ri.Recipe)
                .HasForeignKey(ri => ri.RecipeId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
