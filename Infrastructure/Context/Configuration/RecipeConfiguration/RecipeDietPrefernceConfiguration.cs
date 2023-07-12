using Domain.Entities.RecipeEntities;
using Domain.Enums;
using Infrastructure.Setting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Context.Configuration.RecipeConfiguration
{
    public class RecipeDietPreferenceConfiguration : IEntityTypeConfiguration<RecipeDietPreference>
    {
        public void Configure(EntityTypeBuilder<RecipeDietPreference> builder)
        {
            builder.ToTable(nameof(RecipeDietPreference), DatabaseSetting.RecipeSchema); // todo : implement DependencyInjection  and service creation approach 

            builder.HasKey(x => x.Id);

            builder.Property(x => x.DietaryPreferences)
                .IsRequired();

            builder.HasData(Enum.GetValues(typeof(DietaryPreferencesEnum))
                .Cast<DietaryPreferencesEnum>()
                .Select(e => new RecipeDietPreference
                {
                    Id = (int)e,
                    DietaryPreferences = e
                }));
        }
    }
}
