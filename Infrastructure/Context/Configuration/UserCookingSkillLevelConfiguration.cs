using Domain.Entities.UserEntities;
using Infrastructure.Setting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Context.Configuration
{
    public class UserCookingSkillLevelConfiguration : IEntityTypeConfiguration<UserCookingSkillLevel>
    {
        public void Configure(EntityTypeBuilder<UserCookingSkillLevel> builder)
        {
            builder.ToTable(nameof(UserCookingSkillLevel), DatabaseSetting.UserSchema);
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("SkillLevelId"); 
        }
    }
}
