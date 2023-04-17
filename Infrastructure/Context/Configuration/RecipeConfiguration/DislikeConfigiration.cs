using Domain.Entities.RecipeEntities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Context.Configuration.RecipeConfiguration
{
    public class DislikeConfiguration : IEntityTypeConfiguration<Dislike>
    {
        public void Configure(EntityTypeBuilder<Dislike> builder)
        {
            builder.ToTable(nameof(Dislike));
            builder.HasKey(x => x.Id);
            builder.Property(p => p.Name)
                .HasMaxLength(256)
                .IsRequired(false);
        }
    }
}
