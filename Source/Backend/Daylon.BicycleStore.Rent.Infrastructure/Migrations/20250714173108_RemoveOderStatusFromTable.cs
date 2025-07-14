using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Daylon.BicycleStore.Rent.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemoveOderStatusFromTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrderStatus",
                table: "Bicycles");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OrderStatus",
                table: "Bicycles",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
