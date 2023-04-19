using Domain.Entities.RecipeEntities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Setting;
using Domain.ValueObjects.Recipe;
using System.Reflection.Emit;

namespace Infrastructure.Context.Configuration.RecipeConfiguration
{
    public class GeneratedRecipeConfiguration : IEntityTypeConfiguration<GeneratedRecipe>
    {
        public void Configure(EntityTypeBuilder<GeneratedRecipe> builder)
        {
            builder.ToTable(nameof(GeneratedRecipe), DatabaseSetting.RecipeSchema);

            builder.HasKey(x => x.GeneratedRecipeID);

            builder.Property(x => x.GeneratedRecipeID).HasColumnName("GeneratedRecipeID").IsRequired();


            // Navigation property for FoodInformation
            builder.HasOne(x => x.FoodInformation)
                .WithOne()
                .HasForeignKey<FoodInformation>(x => x.Id)
                .IsRequired();
        }
    }
}
