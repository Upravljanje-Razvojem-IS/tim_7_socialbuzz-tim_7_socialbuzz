﻿// <auto-generated />
using System;
using AdMicroservice.DBContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AdMicroservice.Migrations
{
    [DbContext(typeof(ItemForSaleDbContext))]
    [Migration("20210807131109_initial")]
    partial class initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.8")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("AdMicroservice.Entities.PastPrice", b =>
                {
                    b.Property<int>("PastPriceId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<Guid>("ItemForSaleId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Price")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PastPriceId");

                    b.ToTable("PastPrices");

                    b.HasData(
                        new
                        {
                            PastPriceId = 1,
                            ItemForSaleId = new Guid("4f29d0a1-a000-4b56-9005-1a40ffcea3ae"),
                            Price = "36999.00 RSD"
                        },
                        new
                        {
                            PastPriceId = 2,
                            ItemForSaleId = new Guid("86f5ae7c-ef07-4339-9f46-c8f597560565"),
                            Price = "15600.00 RSD"
                        },
                        new
                        {
                            PastPriceId = 3,
                            ItemForSaleId = new Guid("1f4aa5b3-a67f-45c5-b519-771a7c09a944"),
                            Price = "3200.00 RSD"
                        },
                        new
                        {
                            PastPriceId = 4,
                            ItemForSaleId = new Guid("2d53fc22-eac4-43bb-8f55-d2b8495603cc"),
                            Price = "2099.00 RSD"
                        });
                });

            modelBuilder.Entity("AdMicroservice.Entities.Product", b =>
                {
                    b.Property<Guid>("ItemForSaleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AccountId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Price")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Weight")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ItemForSaleId");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            ItemForSaleId = new Guid("4f29d0a1-a000-4b56-9005-1a40ffcea3ae"),
                            AccountId = new Guid("f2d8362a-124f-41a9-a22b-6e35b3a2953c"),
                            Description = "Huawei P30 Pro, 16gb RAM, 64gb memorije, dual sim.",
                            Name = "Mobilni telefon Huawei P30 Pro",
                            Price = "32000.00 RSD",
                            Weight = "165g"
                        },
                        new
                        {
                            ItemForSaleId = new Guid("86f5ae7c-ef07-4339-9f46-c8f597560565"),
                            AccountId = new Guid("1bc6929f-0e75-4bef-a835-7dbb50d9e41a"),
                            Description = "﻿﻿﻿Stevens Jazz bicikl u odličnom stanju, točkovi 28, aluminijumski ram, dinamo u prednjem točku, led svetla, menjač Shimano Acera.",
                            Name = "Stevens Jazz bicikl",
                            Price = "16500.00 RSD",
                            Weight = "7.1kg"
                        });
                });

            modelBuilder.Entity("AdMicroservice.Entities.Service", b =>
                {
                    b.Property<Guid>("ItemForSaleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AccountId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Price")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ItemForSaleId");

                    b.ToTable("Services");

                    b.HasData(
                        new
                        {
                            ItemForSaleId = new Guid("1f4aa5b3-a67f-45c5-b519-771a7c09a944"),
                            AccountId = new Guid("b1d1e043-85c9-4ee1-9eb7-38314c109607"),
                            Description = "﻿﻿﻿Hit nokti za leto 2021. godine su svakako nokti veselih i vedrih boja. Dodjite i uverite se u nas kvaltet.",
                            Name = "Izlivanje noktiju",
                            Price = "3500.00 RSD"
                        },
                        new
                        {
                            ItemForSaleId = new Guid("2d53fc22-eac4-43bb-8f55-d2b8495603cc"),
                            AccountId = new Guid("9888cf22-b353-4162-aedc-734ca2dc26a4"),
                            Description = "﻿﻿﻿Relax, sportska, parcijalna, teraputska i antistres u prijatnom prostoru sa prijatnim ljudima, dođite i opustite se.",
                            Name = "Masaza",
                            Price = "2700.00 RSD"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
