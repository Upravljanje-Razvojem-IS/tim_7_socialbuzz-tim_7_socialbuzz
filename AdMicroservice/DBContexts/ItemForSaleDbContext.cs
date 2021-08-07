using AdMicroservice.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdMicroservice.DBContexts
{
    public class ItemForSaleDbContext : DbContext
    {
        private readonly IConfiguration configuration;

        public ItemForSaleDbContext(DbContextOptions options, IConfiguration configuration) : base(options)
        {
            this.configuration = configuration;
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<PastPrice> PastPrices { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("AdMicroserviceDb"));
        }

        /// <summary>
        /// Filling the database with some data
        /// </summary>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasData(
                new
                {
                    ItemForSaleId = Guid.Parse("4f29d0a1-a000-4b56-9005-1a40ffcea3ae"),
                    Name = "Mobilni telefon Huawei P30 Pro",
                    Description = "Huawei P30 Pro, 16gb RAM, 64gb memorije, dual sim.",
                    Price = "32000.00 RSD",
                    AccountId = Guid.Parse("f2d8362a-124f-41a9-a22b-6e35b3a2953c"),
                    Weight = "165g"
                },
                new
                {
                    ItemForSaleId = Guid.Parse("86f5ae7c-ef07-4339-9f46-c8f597560565"),
                    Name = "Stevens Jazz bicikl",
                    Description = "﻿﻿﻿Stevens Jazz bicikl u odličnom stanju, točkovi 28, aluminijumski ram, dinamo u prednjem točku, led svetla, menjač Shimano Acera.",
                    Price = "16500.00 RSD",
                    AccountId = Guid.Parse("1bc6929f-0e75-4bef-a835-7dbb50d9e41a"),
                    Weight = "7.1kg"
                });

            modelBuilder.Entity<Service>().HasData(
               new
               {
                   ItemForSaleId = Guid.Parse("1f4aa5b3-a67f-45c5-b519-771a7c09a944"),
                   Name = "Izlivanje noktiju",
                   Description = "﻿﻿﻿Hit nokti za leto 2021. godine su svakako nokti veselih i vedrih boja. Dodjite i uverite se u nas kvaltet.",
                   Price = "3500.00 RSD",
                   AccountId = Guid.Parse("b1d1e043-85c9-4ee1-9eb7-38314c109607")
               },
               new
               {
                   ItemForSaleId = Guid.Parse("2d53fc22-eac4-43bb-8f55-d2b8495603cc"),
                   Name = "Masaza",
                   Description = "﻿﻿﻿Relax, sportska, parcijalna, teraputska i antistres u prijatnom prostoru sa prijatnim ljudima, dođite i opustite se.",
                   Price = "2700.00 RSD",
                   AccountId = Guid.Parse("9888cf22-b353-4162-aedc-734ca2dc26a4")
               });

            modelBuilder.Entity<PastPrice>().HasData(
                new
                {
                    PastPriceId = 1,
                    ItemForSaleId = Guid.Parse("4f29d0a1-a000-4b56-9005-1a40ffcea3ae"),
                    Price = "36999.00 RSD"
                },
                new
                {
                    PastPriceId = 2,
                    ItemForSaleId = Guid.Parse("86f5ae7c-ef07-4339-9f46-c8f597560565"),
                    Price = "15600.00 RSD"
                },
                new
                {
                    PastPriceId = 3,
                    ItemForSaleId = Guid.Parse("1f4aa5b3-a67f-45c5-b519-771a7c09a944"),
                    Price = "3200.00 RSD"
                },
                new
                {
                    PastPriceId = 4,
                    ItemForSaleId = Guid.Parse("2d53fc22-eac4-43bb-8f55-d2b8495603cc"),
                    Price = "2099.00 RSD"
                });

        }
    }
}
