using Domain.Entities.RecipeEntities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Setting;
using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Context.Configuration.RecipeConfiguration
{
    public class FoodInformationConfiguration : IEntityTypeConfiguration<FoodInformation>
    {
        public void Configure(EntityTypeBuilder<FoodInformation> builder)
        {
            builder.ToTable(nameof(FoodInformation), DatabaseSetting.RecipeSchema);
            builder.HasKey(fi => fi.Id);

            builder.Property(fi => fi.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(fi => fi.Description)
                .IsRequired()
                .HasMaxLength(256);

            builder.Property(fi => fi.PreparationTime)
                  .IsRequired()
                   .HasColumnType("Time");

            builder.Property(fi => fi.CookingTime)
                .IsRequired();

            builder.Property(fi => fi.Servings)
                .IsRequired();

            builder.Property(fi => fi.CaloriesPerServing)
                .IsRequired();

            builder.Property(fi => fi.ServingSize)
                .IsRequired();

            builder.Property(fi => fi.DietaryPreferences)
                .HasMaxLength(64);

            builder.Property(fi => fi.KeyIngredients)
                .HasMaxLength(64);

            builder.Property(fi => fi.AllergyRestrictions)
                .HasMaxLength(64);

            builder.Property(fi => fi.Cuisine)
                .HasMaxLength(64);

            builder.Property(fi => fi.DishType)
                .HasMaxLength(64);

            builder.Property(fi => fi.CookingMethod)
                .HasMaxLength(64);
        }
    }

}
