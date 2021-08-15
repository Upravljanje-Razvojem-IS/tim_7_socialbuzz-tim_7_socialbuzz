using MediaMicroservice.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediaMicroservice.DBContexts
{
    public class MediaDbContext : DbContext
    {
        private readonly IConfiguration configuration;

        public MediaDbContext(DbContextOptions options, IConfiguration configuration) : base(options)
        {
            this.configuration = configuration;
        }

        public DbSet<Media> Medias { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("MediaMicroserviceDb"));
        }

        /// <summary>
        /// Filling the database with some data
        /// </summary>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Media>().HasData(
                new
                {
                    MediaId = Guid.Parse("11b74292-ff41-4ecd-873c-f81fd88692fe"),
                    FilePath = "https://static.tehnomanija.rs/UserFiles/products/2019/006/large/137147.webp",
                    ItemForSaleId = Guid.Parse("4f29d0a1-a000-4b56-9005-1a40ffcea3ae"),
                    AccountId = Guid.Parse("f2d8362a-124f-41a9-a22b-6e35b3a2953c")
                },
                new
                {
                    MediaId = Guid.Parse("91cc2b07-a231-4fe7-bf3b-48821e35c904"),
                    FilePath = "https://www.stevensbikes.de/2019/img/slider_res/jazz_gent_19_55_slate_grey_my19.jpg",
                    ItemForSaleId = Guid.Parse("86f5ae7c-ef07-4339-9f46-c8f597560565"),
                    AccountId = Guid.Parse("1bc6929f-0e75-4bef-a835-7dbb50d9e41a")
                },
                new
                {
                    MediaId = Guid.Parse("6a21bf33-1b44-4846-975c-e91a9fba0203"),
                    FilePath = "https://static.sredime.rs/image/photo/1/b/1b9f38268c50805669fd8caf8f3cc84a/3574_lg.jpg",
                    ItemForSaleId = Guid.Parse("1f4aa5b3-a67f-45c5-b519-771a7c09a944"),
                    AccountId = Guid.Parse("b1d1e043-85c9-4ee1-9eb7-38314c109607")
                });
        }

    }
}
