using Domain.Entities.RecipeEntities;
using Domain.Enums.RecipeEnums;
using Infrastructure.Setting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Context.Configuration.RecipeConfiguration
{
    public class MealTypeConfiguration : IEntityTypeConfiguration<MealType>
    {
        public void Configure(EntityTypeBuilder<MealType> builder)
        {
            builder.ToTable(nameof(MealType), DatabaseSetting.RecipeSchema); // todo : implement DependencyInjection  and service creation approach 

            builder.HasKey(x => x.Id);

            builder.Property(x => x.MealName)
                .IsRequired()
                .HasMaxLength(50);

            builder.HasData(Enum.GetValues(typeof(MealTypeEnum))
                  .Cast<MealTypeEnum>()
                  .Select(e => new MealType
                  {
                      Id = (int)e,
                      MealName = e
                  }));
        }
    }
}
