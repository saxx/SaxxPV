using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SaxxPv.Web.Migrations
{
    /// <inheritdoc />
    public partial class DayBattery_and_TotalImportExport : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "DayBatteryCharge",
                table: "Readings",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "DayBatteryDischarge",
                table: "Readings",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "TotalExport",
                table: "Readings",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "TotalImport",
                table: "Readings",
                type: "float",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DayBatteryCharge",
                table: "Readings");

            migrationBuilder.DropColumn(
                name: "DayBatteryDischarge",
                table: "Readings");

            migrationBuilder.DropColumn(
                name: "TotalExport",
                table: "Readings");

            migrationBuilder.DropColumn(
                name: "TotalImport",
                table: "Readings");
        }
    }
}
