using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Context.Configuration
{
    public class UserAllergyConfiguration : IEntityTypeConfiguration<UserAllergy>
    {
        public void Configure(EntityTypeBuilder<UserAllergy> builder)
        {
            builder.ToTable(nameof(UserAllergy), "User"); 
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("AllergyID");            
        }
    }
}
