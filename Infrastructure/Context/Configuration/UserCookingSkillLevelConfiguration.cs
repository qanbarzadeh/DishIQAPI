using Domain.Entities.UserEntities;
using Infrastructure.Setting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Context.Configuration
{
    public class UserCookingSkillLevelConfiguration : IEntityTypeConfiguration<UserCookingSkillLevel>
    {
        public void Configure(EntityTypeBuilder<UserCookingSkillLevel> builder)
        {
            builder.ToTable(nameof(UserCookingSkillLevel), DatabaseSetting.UserSchema);
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("SkillLevelId");


            builder.HasOne<ApplicationUser>()
            .WithOne(x => x.UserCookingSkillLevel)
            .HasForeignKey<UserCookingSkillLevel>(x => x.UserId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired(); 
            //If every UserCookingSkillLevel should always have a User
        }
    }
}
