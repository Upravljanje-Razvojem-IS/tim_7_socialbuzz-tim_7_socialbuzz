using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RatingMicroservice.Migrations
{
    public partial class Createdatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Ratings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DateOfTrade = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RatingDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ItemRating = table.Column<int>(type: "int", nullable: false),
                    SellerRating = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ItemId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ratings", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ratings");
        }
    }
}
