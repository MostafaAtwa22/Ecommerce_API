using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecommerce.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CacheTbl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ClientSecret",
                table: "CustomerBaskets",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "DeliveryMethodId",
                table: "CustomerBaskets",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PaymentIntentId",
                table: "CustomerBaskets",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "CacheEntries",
                columns: table => new
                {
                    CacheKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SerializedResponse = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExpiryTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CacheEntries", x => x.CacheKey);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CacheEntries");

            migrationBuilder.DropColumn(
                name: "ClientSecret",
                table: "CustomerBaskets");

            migrationBuilder.DropColumn(
                name: "DeliveryMethodId",
                table: "CustomerBaskets");

            migrationBuilder.DropColumn(
                name: "PaymentIntentId",
                table: "CustomerBaskets");
        }
    }
}
