using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AdMicroservice.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PastPrices",
                columns: table => new
                {
                    PastPriceId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemForSaleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Price = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PastPrices", x => x.PastPriceId);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ItemForSaleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Weight = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    Price = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ItemForSaleId);
                });

            migrationBuilder.CreateTable(
                name: "Services",
                columns: table => new
                {
                    ItemForSaleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    Price = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Services", x => x.ItemForSaleId);
                });

            migrationBuilder.InsertData(
                table: "PastPrices",
                columns: new[] { "PastPriceId", "ItemForSaleId", "Price" },
                values: new object[,]
                {
                    { 1, new Guid("4f29d0a1-a000-4b56-9005-1a40ffcea3ae"), "36999.00 RSD" },
                    { 2, new Guid("86f5ae7c-ef07-4339-9f46-c8f597560565"), "15600.00 RSD" },
                    { 3, new Guid("1f4aa5b3-a67f-45c5-b519-771a7c09a944"), "3200.00 RSD" },
                    { 4, new Guid("2d53fc22-eac4-43bb-8f55-d2b8495603cc"), "2099.00 RSD" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ItemForSaleId", "AccountId", "Description", "Name", "Price", "Weight" },
                values: new object[,]
                {
                    { new Guid("4f29d0a1-a000-4b56-9005-1a40ffcea3ae"), new Guid("f2d8362a-124f-41a9-a22b-6e35b3a2953c"), "Huawei P30 Pro, 16gb RAM, 64gb memorije, dual sim.", "Mobilni telefon Huawei P30 Pro", "32000.00 RSD", "165g" },
                    { new Guid("86f5ae7c-ef07-4339-9f46-c8f597560565"), new Guid("1bc6929f-0e75-4bef-a835-7dbb50d9e41a"), "﻿﻿﻿Stevens Jazz bicikl u odličnom stanju, točkovi 28, aluminijumski ram, dinamo u prednjem točku, led svetla, menjač Shimano Acera.", "Stevens Jazz bicikl", "16500.00 RSD", "7.1kg" }
                });

            migrationBuilder.InsertData(
                table: "Services",
                columns: new[] { "ItemForSaleId", "AccountId", "Description", "Name", "Price" },
                values: new object[,]
                {
                    { new Guid("1f4aa5b3-a67f-45c5-b519-771a7c09a944"), new Guid("b1d1e043-85c9-4ee1-9eb7-38314c109607"), "﻿﻿﻿Hit nokti za leto 2021. godine su svakako nokti veselih i vedrih boja. Dodjite i uverite se u nas kvaltet.", "Izlivanje noktiju", "3500.00 RSD" },
                    { new Guid("2d53fc22-eac4-43bb-8f55-d2b8495603cc"), new Guid("9888cf22-b353-4162-aedc-734ca2dc26a4"), "﻿﻿﻿Relax, sportska, parcijalna, teraputska i antistres u prijatnom prostoru sa prijatnim ljudima, dođite i opustite se.", "Masaza", "2700.00 RSD" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PastPrices");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Services");
        }
    }
}
