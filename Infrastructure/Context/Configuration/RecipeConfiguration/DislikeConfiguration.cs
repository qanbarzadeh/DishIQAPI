using Domain.Entities.RecipeEntities;
using Infrastructure.Setting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Context.Configuration.RecipeConfiguration
{
    public class DislikeConfiguration : IEntityTypeConfiguration<Dislike>
    {
        public void Configure(EntityTypeBuilder<Dislike> builder)
        {
            builder.ToTable(nameof(Dislike), DatabaseSetting.RecipeSchema); // todo : implement DependencyInjection  and service creation approach 

            builder.HasKey(x => x.Id);
            builder.Property(p => p.Name)
                .HasMaxLength(256)
                .IsRequired(false);
        }
    }
}


