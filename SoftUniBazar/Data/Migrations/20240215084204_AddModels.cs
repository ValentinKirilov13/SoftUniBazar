using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SoftUniBazar.Data.Migrations
{
    public partial class AddModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Category Identifier)")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false, comment: "Category Name)")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                },
                comment: "Table of Categories)");

            migrationBuilder.CreateTable(
                name: "Ads",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Ad Identifier")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false, comment: "Ad Name"),
                    Description = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false, comment: "Ad Description"),
                    Price = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false, comment: "Ad Price"),
                    OwnerId = table.Column<string>(type: "nvarchar(450)", nullable: false, comment: "Ad Owner Identifier"),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "Ad Img"),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Ad Created On"),
                    CategoryId = table.Column<int>(type: "int", nullable: false, comment: "Ad Category Identifier")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ads", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ads_AspNetUsers_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ads_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "Table of Ads");

            migrationBuilder.CreateTable(
                name: "AdsBuyers",
                columns: table => new
                {
                    BuyerId = table.Column<string>(type: "nvarchar(450)", nullable: false, comment: "Buyer Identifier"),
                    AdId = table.Column<int>(type: "int", nullable: false, comment: "Ad Identifier")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdsBuyers", x => new { x.BuyerId, x.AdId });
                    table.ForeignKey(
                        name: "FK_AdsBuyers_Ads_AdId",
                        column: x => x.AdId,
                        principalTable: "Ads",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AdsBuyers_AspNetUsers_BuyerId",
                        column: x => x.BuyerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "Table of Ad Buyers");

            migrationBuilder.CreateIndex(
                name: "IX_Ads_CategoryId",
                table: "Ads",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Ads_OwnerId",
                table: "Ads",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_AdsBuyers_AdId",
                table: "AdsBuyers",
                column: "AdId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdsBuyers");

            migrationBuilder.DropTable(
                name: "Ads");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
