using Domain.Entities.RecipeEntities;
using Infrastructure.Setting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Context.Configuration.RecipeConfiguration
{
    public class CookingTechniqueConfiguration : IEntityTypeConfiguration<CookingTechnique>
    {
        public void Configure(EntityTypeBuilder<CookingTechnique> builder)
        {
            builder.ToTable(nameof(CookingTechnique), DatabaseSetting.RecipeSchema); // todo : implement DependencyInjection  and service creation approach 


            builder.HasKey(x => x.Id);
            builder.Property(p => p.Name)
                .HasMaxLength(100)
                .IsRequired();
        }
    }
}
