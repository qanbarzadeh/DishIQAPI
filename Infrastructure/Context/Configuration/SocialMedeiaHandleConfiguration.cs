using Domain.Entities.UserEntities;
using Infrastructure.Setting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Context.Configuration
{
    public class SocialHandleConfiguration : IEntityTypeConfiguration<SocialMediaHandle>
    {
        public void Configure(EntityTypeBuilder<SocialMediaHandle> builder)
        {
            builder.ToTable(nameof(SocialHandleConfiguration), DatabaseSetting.Schema);
            builder.HasKey(k => k.Id);
            builder.Property(s => s.Id)
                .HasColumnName("SocialMediaId");
            // Table mapping
            builder.ToTable("SocialMediaHandles");

            // Primary key
            builder.HasKey(sm => sm.Id);

            // Social media type
            builder.Property(sm => sm.Type)
                .IsRequired();
            // Social media handle
            builder.Property(sm => sm.Handle)
                .IsRequired();                        
        }
    }
}
