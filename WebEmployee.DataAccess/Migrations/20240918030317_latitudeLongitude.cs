using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebEmployee.web.Migrations
{
    /// <inheritdoc />
    public partial class latitudeLongitude : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "Latitude",
                table: "Employees",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "Longitude",
                table: "Employees",
                type: "real",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Latitude",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "Longitude",
                table: "Employees");
        }
    }
}
