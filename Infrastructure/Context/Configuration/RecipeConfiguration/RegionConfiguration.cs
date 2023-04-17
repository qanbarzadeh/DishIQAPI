using Domain.Entities.RecipeEntities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Domain.Enums.RecipeEnums;

namespace Infrastructure.Context.Configuration.RecipeConfiguration
{
    public class RegionConfiguration : IEntityTypeConfiguration<Region>
    {
        public void Configure(EntityTypeBuilder<Region> builder)
        {
            builder.ToTable(nameof(Region));
            builder.HasKey(x => x.Id);

            builder.HasData(Enum.GetValues(typeof(RegionEnum))
                .Cast<RegionEnum>()
                .Select(e => new Region
                {
                    Id = (int)e,
                    RegionName = e
                }));
        }
    }
}
