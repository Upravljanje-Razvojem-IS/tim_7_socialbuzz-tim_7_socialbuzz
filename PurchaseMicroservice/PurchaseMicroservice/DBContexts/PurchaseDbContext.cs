using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PurchaseMicroservice.Entities;

namespace PurchaseMicroservice.DBContexts
{
    public class PurchaseDbContext : DbContext
    {

        private readonly IConfiguration configuration;

        public PurchaseDbContext(DbContextOptions options, IConfiguration configuration) : base(options)
        {
            this.configuration = configuration;
        }

        public DbSet<Purchase> Purchases { get; set; }

        protected override void OnConfiguring (DbContextOptionsBuilder contextOptionsBuilder)
        {
            contextOptionsBuilder.UseSqlServer(configuration.GetConnectionString("PurchaseMicroserviceDb"));
        }

      protected override void OnModelCreating (ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Purchase>().HasData(
                new
                {
                    PurchaseId = Guid.Parse("36e745c1-8615-4244-bf16-492b6493602f"),
                    Date = "05/29/2021",
                    Description = "Cash on delivery",
                    AccountId = Guid.Parse("7e58590d-3968-4730-9c62-1ce5ae071478"),
                    DeliveryId = Guid.Parse("e04ce688-4fe1-43a1-838e-e3aedbe083b8"),
                    ItemForSaleId = Guid.Parse("915510b2-74fb-44b7-b265-730ac0079a0d")
                },
                new
                {
                    PurchaseId = Guid.Parse("ac770b34-0a5f-46f4-b5d0-7c4a7ce3a036"),
                    Date = "06/11/2021",
                    Description = "Cash on delivery",
                    AccountId = Guid.Parse("1265bcd7-6e5a-491b-b802-acf4c4646d78"),
                    DeliveryId = Guid.Parse("bea55668-1ab8-4aa7-aca3-9b184f2c80d1"),
                    ItemForSaleId = Guid.Parse("972379ca-7d10-4741-9b61-90414c77f32d")
                },
                new
                {
                    PurchaseId = Guid.Parse("fc953708-ce8b-4925-bc2e-a1ef4b8fc932"),
                    Date = "06/07/2021",
                    Description = "Cash on delivery",
                    AccountId = Guid.Parse("bcac549f-188c-415b-b64f-6ac5545a3c17"),
                    DeliveryId = Guid.Parse("f3c1d7f0-f973-4e11-9d99-35bf23f88304"),
                    ItemForSaleId = Guid.Parse("fe7a9096-80f4-4b7f-a8c3-0029be9959b7")
                }
                );
        }
    }
}
