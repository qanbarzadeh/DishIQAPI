using Domain.Entities.RecipeEntities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities.UserEntities;
using Infrastructure.Setting;

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
