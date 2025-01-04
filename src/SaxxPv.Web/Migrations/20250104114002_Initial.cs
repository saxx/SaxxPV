using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SaxxPv.Web.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Pricings",
                columns: table => new
                {
                    From = table.Column<DateTime>(type: "datetime2", nullable: false),
                    To = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BuyPrice = table.Column<double>(type: "float", nullable: false),
                    SellPrice = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pricings", x => new { x.From, x.To });
                });

            migrationBuilder.CreateTable(
                name: "Readings",
                columns: table => new
                {
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CurrentLoad = table.Column<double>(type: "float", nullable: false),
                    CurrentPv = table.Column<double>(type: "float", nullable: false),
                    CurrentGrid = table.Column<double>(type: "float", nullable: false),
                    CurrentBattery = table.Column<double>(type: "float", nullable: false),
                    CurrentBatterySoc = table.Column<double>(type: "float", nullable: false),
                    DayTotal = table.Column<double>(type: "float", nullable: false),
                    DayBought = table.Column<double>(type: "float", nullable: false),
                    DaySold = table.Column<double>(type: "float", nullable: false),
                    DayConsumption = table.Column<double>(type: "float", nullable: false),
                    DaySelfUse = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Readings", x => x.DateTime);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Pricings");

            migrationBuilder.DropTable(
                name: "Readings");
        }
    }
}
