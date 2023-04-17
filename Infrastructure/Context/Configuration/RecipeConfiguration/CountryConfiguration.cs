using Domain.Entities.RecipeEntities;
using Domain.Entities.UserEntities;
using Infrastructure.Setting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
