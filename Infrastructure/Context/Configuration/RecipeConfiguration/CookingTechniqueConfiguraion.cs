using Domain.Entities.RecipeEntities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Context.Configuration.RecipeConfiguration
{
    public class CookingTechniqueConfiguration : IEntityTypeConfiguration<CookingTechnique>
    {
        public void Configure(EntityTypeBuilder<CookingTechnique> builder)
        {
            builder.ToTable(nameof(CookingTechnique));
            builder.HasKey(x => x.Id);
            builder.Property(p => p.Name)
                .HasMaxLength(100)
                .IsRequired();
        }
    }
}
