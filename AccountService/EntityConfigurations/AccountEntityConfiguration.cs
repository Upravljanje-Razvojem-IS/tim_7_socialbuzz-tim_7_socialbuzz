using AccountService.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AccountService.EntityConfigurations
{
    public class AccountEntityConfiguration : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder
                .ToTable("Accounts")
                .HasKey(a => a.Id);

            builder
                .HasOne(a => a.Role)
                .WithMany(ar => ar.Accounts)
                .IsRequired(true)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasOne(a => a.City)
                .WithMany(c => c.Accounts)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
