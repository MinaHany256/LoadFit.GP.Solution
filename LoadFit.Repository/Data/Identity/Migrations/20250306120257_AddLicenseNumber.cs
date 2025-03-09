using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LoadFit.Repository.Data.Identity.Migrations
{
    /// <inheritdoc />
    public partial class AddLicenseNumber : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LicenseNumber",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LicenseNumber",
                table: "AspNetUsers");
        }
    }
}
