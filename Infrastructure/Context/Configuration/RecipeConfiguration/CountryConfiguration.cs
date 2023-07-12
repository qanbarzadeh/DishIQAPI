using Domain.Entities.RecipeEntities;
using Infrastructure.Setting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Context.Configuration.RecipeConfiguration
{
    public class CountryConfiguration : IEntityTypeConfiguration<Country>
    {
        public void Configure(EntityTypeBuilder<Country> builder)
        {
            builder.ToTable(nameof(Country), DatabaseSetting.RecipeSchema); // todo : implement DependencyInjection  and service creation approach 

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name)
                .HasMaxLength(128)
                .IsRequired();
        }
    }
}
