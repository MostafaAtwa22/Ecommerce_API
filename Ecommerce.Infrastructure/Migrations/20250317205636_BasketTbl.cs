﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecommerce.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class BasketTbl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CustomerBaskets",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerBaskets", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BasketItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    PictureUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Brand = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CustomerBasketId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BasketItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BasketItems_CustomerBaskets_CustomerBasketId",
                        column: x => x.CustomerBasketId,
                        principalTable: "CustomerBaskets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BasketItems_CustomerBasketId",
                table: "BasketItems",
                column: "CustomerBasketId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BasketItems");

            migrationBuilder.DropTable(
                name: "CustomerBaskets");
        }
    }
}
