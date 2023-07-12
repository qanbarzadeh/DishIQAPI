using Domain.Entities.RecipeEntities;
using Infrastructure.Setting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Context.Configuration.RecipeConfiguration
{
    public class CookingStepConfiguration : IEntityTypeConfiguration<CookingStep>
    {
        public void Configure(EntityTypeBuilder<CookingStep> builder)
        {
            builder.ToTable(nameof(CookingStep), DatabaseSetting.RecipeSchema); // todo : implement DependencyInjection  and service creation approach 

            builder.HasKey(x => x.Id);
            builder.Property(p => p.Description)
                .HasMaxLength(1000)
                .IsRequired();

            builder.Property(o => o.Order)
                .IsRequired()
                .HasDefaultValue(0);
        }
    }
}
