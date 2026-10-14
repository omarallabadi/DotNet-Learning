using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace StockApp_V6.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BuyOrders",
                columns: table => new
                {
                    BuyOrderID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StockSymbol = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StockName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateAndTimeOfOrder = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Quantity = table.Column<long>(type: "bigint", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BuyOrders", x => x.BuyOrderID);
                });

            migrationBuilder.CreateTable(
                name: "SellOrders",
                columns: table => new
                {
                    SellOrderID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StockSymbol = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StockName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateAndTimeOfOrder = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Quantity = table.Column<long>(type: "bigint", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SellOrders", x => x.SellOrderID);
                });

            migrationBuilder.InsertData(
                table: "BuyOrders",
                columns: new[] { "BuyOrderID", "DateAndTimeOfOrder", "Price", "Quantity", "StockName", "StockSymbol" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-1111-1111-111111111111"), new DateTime(2024, 1, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), 150.5, 100L, "Apple Inc.", "AAPL" },
                    { new Guid("11111111-1111-1111-1111-111111111112"), new DateTime(2024, 1, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), 300.25, 500L, "Microsoft Corporation", "MSFT" },
                    { new Guid("11111111-1111-1111-1111-111111111113"), new DateTime(2024, 1, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), 175.75, 1000L, "Alphabet Inc.", "GOOGL" },
                    { new Guid("11111111-1111-1111-1111-111111111114"), new DateTime(2024, 1, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), 180.0, 2500L, "Amazon.com Inc.", "AMZN" },
                    { new Guid("11111111-1111-1111-1111-111111111115"), new DateTime(2024, 1, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), 250.5, 10000L, "Tesla Inc.", "TSLA" }
                });

            migrationBuilder.InsertData(
                table: "SellOrders",
                columns: new[] { "SellOrderID", "DateAndTimeOfOrder", "Price", "Quantity", "StockName", "StockSymbol" },
                values: new object[,]
                {
                    { new Guid("22222222-2222-2222-2222-222222222221"), new DateTime(2024, 1, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), 150.5, 100L, "Apple Inc.", "AAPL" },
                    { new Guid("22222222-2222-2222-2222-222222222222"), new DateTime(2024, 1, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), 300.25, 500L, "Microsoft Corporation", "MSFT" },
                    { new Guid("22222222-2222-2222-2222-222222222223"), new DateTime(2024, 1, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), 175.75, 1000L, "Alphabet Inc.", "GOOGL" },
                    { new Guid("22222222-2222-2222-2222-222222222224"), new DateTime(2024, 1, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), 180.0, 2500L, "Amazon.com Inc.", "AMZN" },
                    { new Guid("22222222-2222-2222-2222-222222222225"), new DateTime(2024, 1, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), 250.5, 10000L, "Tesla Inc.", "TSLA" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BuyOrders");

            migrationBuilder.DropTable(
                name: "SellOrders");
        }
    }
}
