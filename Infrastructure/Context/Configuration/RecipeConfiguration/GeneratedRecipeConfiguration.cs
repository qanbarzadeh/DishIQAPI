using Domain.Entities.RecipeEntities;
using Infrastructure.Setting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

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

            builder.HasMany(x => x.Ingredients)
                .WithOne()
                .HasForeignKey(x => x.Id)
                .IsRequired();

            builder.HasMany(x => x.CookingSteps)
                .WithOne()
                .HasForeignKey(x => x.Id)
                .IsRequired();
        }
    }
}
