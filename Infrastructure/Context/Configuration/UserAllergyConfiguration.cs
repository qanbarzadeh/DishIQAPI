using Domain.Entities.UserEntities;
using Infrastructure.Setting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Context.Configuration
{
    public class UserAllergyConfiguration : IEntityTypeConfiguration<UserAllergy>
    {
        public void Configure(EntityTypeBuilder<UserAllergy> builder)
        {
            builder.ToTable(nameof(UserAllergy), DatabaseSetting.UserSchema);

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(x => x.Description)
                .HasMaxLength(500);

            builder.Property(x => x.SeverityLevel)
                .IsRequired()
                .HasConversion<int>();

            builder.Property(x => x.CreatedAt)
                .HasColumnType("datetime2")
                .HasDefaultValueSql("getutcdate()");

            builder.Property(x => x.UpdatedAt)
                .HasColumnType("datetime2")
                .HasDefaultValueSql("getutcdate()");

            builder.HasOne<ApplicationUser>()
                .WithMany(x => x.UserAllergies)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
