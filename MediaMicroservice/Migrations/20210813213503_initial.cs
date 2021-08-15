using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MediaMicroservice.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Medias",
                columns: table => new
                {
                    MediaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FilePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ItemForSaleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medias", x => x.MediaId);
                });

            migrationBuilder.InsertData(
                table: "Medias",
                columns: new[] { "MediaId", "AccountId", "FilePath", "ItemForSaleId" },
                values: new object[] { new Guid("11b74292-ff41-4ecd-873c-f81fd88692fe"), new Guid("f2d8362a-124f-41a9-a22b-6e35b3a2953c"), "https://static.tehnomanija.rs/UserFiles/products/2019/006/large/137147.webp", new Guid("4f29d0a1-a000-4b56-9005-1a40ffcea3ae") });

            migrationBuilder.InsertData(
                table: "Medias",
                columns: new[] { "MediaId", "AccountId", "FilePath", "ItemForSaleId" },
                values: new object[] { new Guid("91cc2b07-a231-4fe7-bf3b-48821e35c904"), new Guid("1bc6929f-0e75-4bef-a835-7dbb50d9e41a"), "https://www.stevensbikes.de/2019/img/slider_res/jazz_gent_19_55_slate_grey_my19.jpg", new Guid("86f5ae7c-ef07-4339-9f46-c8f597560565") });

            migrationBuilder.InsertData(
                table: "Medias",
                columns: new[] { "MediaId", "AccountId", "FilePath", "ItemForSaleId" },
                values: new object[] { new Guid("6a21bf33-1b44-4846-975c-e91a9fba0203"), new Guid("b1d1e043-85c9-4ee1-9eb7-38314c109607"), "https://static.sredime.rs/image/photo/1/b/1b9f38268c50805669fd8caf8f3cc84a/3574_lg.jpg", new Guid("1f4aa5b3-a67f-45c5-b519-771a7c09a944") });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Medias");
        }
    }
}
