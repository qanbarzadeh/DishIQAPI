using Domain.Entities.RecipeEntities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Context.Configuration.RecipeConfiguration
{
    public class RecipeDietPreferenceConfiguration : IEntityTypeConfiguration<RecipeDietPreference>
    {
        public void Configure(EntityTypeBuilder<RecipeDietPreference> builder)
        {
            builder.ToTable(nameof(RecipeDietPreference));
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
