namespace Infrastructure.Context.Configuration
{
    using Domain.Entities.RecipeEntities;
    using global::Infrastructure.Setting;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    namespace Infrastructure.Context.Configuration
    {
        public class NutritionInformationConfiguration : IEntityTypeConfiguration<NutritionInformation>
        {
            public void Configure(EntityTypeBuilder<NutritionInformation> builder)
            {
                builder.ToTable(nameof(NutritionInformation), DatabaseSetting.Schema);

                builder.HasKey(n => n.Id);

                builder.HasOne(n => n.Ingredient)
                    .WithMany()
                    .HasForeignKey(n => n.IngredientId)
                    .IsRequired()
                    .OnDelete(DeleteBehavior.Cascade);

                builder.Property(n => n.Carbohydrate).IsRequired();
                builder.Property(n => n.Protein).IsRequired();
                builder.Property(n => n.Fat).IsRequired();
                builder.Property(n => n.VitaminA).IsRequired();
                builder.Property(n => n.VitaminC).IsRequired();
                builder.Property(n => n.VitaminD).IsRequired();
                builder.Property(n => n.Calcium).IsRequired();
                builder.Property(n => n.Iron).IsRequired();
                builder.Property(n => n.Sodium).IsRequired();
            }
        }
    }
}
