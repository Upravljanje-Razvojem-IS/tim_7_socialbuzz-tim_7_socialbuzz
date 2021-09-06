using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PurchaseMicroservice.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Purchases",
                columns: table => new
                {
                    PurchaseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Date = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DeliveryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ItemForSaleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Purchases", x => x.PurchaseId);
                });

            migrationBuilder.InsertData(
                table: "Purchases",
                columns: new[] { "PurchaseId", "AccountId", "Date", "DeliveryId", "Description", "ItemForSaleId" },
                values: new object[] { new Guid("36e745c1-8615-4244-bf16-492b6493602f"), new Guid("7e58590d-3968-4730-9c62-1ce5ae071478"), "05/29/2021", new Guid("e04ce688-4fe1-43a1-838e-e3aedbe083b8"), "Cash on delivery", new Guid("915510b2-74fb-44b7-b265-730ac0079a0d") });

            migrationBuilder.InsertData(
                table: "Purchases",
                columns: new[] { "PurchaseId", "AccountId", "Date", "DeliveryId", "Description", "ItemForSaleId" },
                values: new object[] { new Guid("ac770b34-0a5f-46f4-b5d0-7c4a7ce3a036"), new Guid("1265bcd7-6e5a-491b-b802-acf4c4646d78"), "06/11/2021", new Guid("bea55668-1ab8-4aa7-aca3-9b184f2c80d1"), "Cash on delivery", new Guid("972379ca-7d10-4741-9b61-90414c77f32d") });

            migrationBuilder.InsertData(
                table: "Purchases",
                columns: new[] { "PurchaseId", "AccountId", "Date", "DeliveryId", "Description", "ItemForSaleId" },
                values: new object[] { new Guid("fc953708-ce8b-4925-bc2e-a1ef4b8fc932"), new Guid("bcac549f-188c-415b-b64f-6ac5545a3c17"), "06/07/2021", new Guid("f3c1d7f0-f973-4e11-9d99-35bf23f88304"), "Cash on delivery", new Guid("fe7a9096-80f4-4b7f-a8c3-0029be9959b7") });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Purchases");
        }
    }
}
