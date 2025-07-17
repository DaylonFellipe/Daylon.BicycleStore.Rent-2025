using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Daylon.BicycleStore.Rent.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UptRentalOrderDropOffTimeTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DropOffTime",
                table: "RentalOrders",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DropOffTime",
                table: "RentalOrders");
        }
    }
}
