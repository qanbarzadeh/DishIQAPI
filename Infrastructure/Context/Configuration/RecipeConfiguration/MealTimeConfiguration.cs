using Domain.Entities.RecipeEntities;
using Domain.Enums.RecipeEnums;
using Infrastructure.Setting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Context.Configuration.RecipeConfiguration
{
    public class MealTimeConfiguration : IEntityTypeConfiguration<MealTime>
    {
        public void Configure(EntityTypeBuilder<MealTime> builder)
        {
            builder.ToTable(nameof(MealTime), DatabaseSetting.RecipeSchema); // todo : implement DependencyInjection  and service creation approach 

            builder.HasKey(x => x.Id);

            builder.Property(x => x.MealTimeEnum)
                .IsRequired()
                .HasConversion<int>();

            builder.HasData(Enum.GetValues(typeof(MealTimeEnum))
                .Cast<MealTimeEnum>()
                .Select(e => new MealTime
                {
                    Id = (int)e,
                    MealTimeEnum = e
                }));
        }
    }
}