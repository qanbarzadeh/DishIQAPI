using Domain.Entities.RecipeEntities;
using Domain.Enums.RecipeEnums;
using Infrastructure.Setting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Context.Configuration.RecipeConfiguration
{
    public class RegionConfiguration : IEntityTypeConfiguration<Region>
    {
        public void Configure(EntityTypeBuilder<Region> builder)
        {
            builder.ToTable(nameof(Region), DatabaseSetting.RecipeSchema); // todo : implement DependencyInjection  and service creation approach 

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
