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
    public class IngredientConfiguration : IEntityTypeConfiguration<Ingredient>
    {
        public void Configure(EntityTypeBuilder<Ingredient> builder)
        {
            builder.ToTable(nameof(Ingredient), DatabaseSetting.RecipeSchema); // todo : implement DependencyInjection  and service creation approach 
            
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(x => x.Unit)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(x => x.Quantity)
                .IsRequired()
                .HasColumnType("decimal(18, 2)");
        }
    }
}
