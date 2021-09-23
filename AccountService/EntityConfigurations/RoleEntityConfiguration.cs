using AccountService.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountService.EntityConfigurations
{
    public class RoleEntityConfiguration : IEntityTypeConfiguration<AccRole>
    {
        public void Configure(EntityTypeBuilder<AccRole> builder)
        {
            builder
                .ToTable("AccRoles");
                //.HasKey(ar => ar.Id);

            //builder
            //    .Property(ar => ar.Name)
            //    .HasMaxLength(30)
            //    .IsRequired();
        }
    }
}
