using AccountService.Entities;
using AccountService.EntityConfigurations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountService.Data
{
    public class AccountServiceContext : IdentityDbContext<Account, AccRole, Guid>
    {
        public AccountServiceContext(DbContextOptions<AccountServiceContext> options) : base(options)
        {
        }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<AccRole> AccRoles { get; set; }
        public DbSet<City> Cities { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new AccountEntityConfiguration());
            modelBuilder.ApplyConfiguration(new RoleEntityConfiguration());
            modelBuilder.ApplyConfiguration(new CityEntityConfiguration());
            //modelBuilder.ApplyConfigurationsFromAssembly(typeof(AccountServiceContext).Assembly);
        }
    }
}
