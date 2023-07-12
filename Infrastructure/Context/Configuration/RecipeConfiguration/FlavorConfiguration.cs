using Domain.Entities.RecipeEntities;
using Infrastructure.Setting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Context.Configuration.RecipeConfiguration
{
    public class FlavorConfiguration : IEntityTypeConfiguration<Flavor>
    {
        public void Configure(EntityTypeBuilder<Flavor> builder)
        {
            builder.ToTable(nameof(Flavor), DatabaseSetting.RecipeSchema); // todo : implement DependencyInjection  and service creation approach 

            builder.HasKey(x => x.Id);
            builder.Property(x => x.FlavorType)
                .IsRequired();

        }

    }
}
