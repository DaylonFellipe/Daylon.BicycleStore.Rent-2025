using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Daylon.BicycleStore.Rent.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateBicycleEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "DailyRate",
                table: "Bicycles",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "OrderStatus",
                table: "Bicycles",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DailyRate",
                table: "Bicycles");

            migrationBuilder.DropColumn(
                name: "OrderStatus",
                table: "Bicycles");
        }
    }
}
