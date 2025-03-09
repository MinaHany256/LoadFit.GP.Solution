using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LoadFit.Repository.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddingVehicleDimenstions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Height",
                table: "Vehicles",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Length",
                table: "Vehicles",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Width",
                table: "Vehicles",
                type: "decimal(18,2)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Height",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "Length",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "Width",
                table: "Vehicles");
        }
    }
}
