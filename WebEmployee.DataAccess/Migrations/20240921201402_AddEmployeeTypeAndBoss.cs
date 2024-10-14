using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebEmployee.web.Migrations
{
    /// <inheritdoc />
    public partial class AddEmployeeTypeAndBoss : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BossId",
                table: "Employees",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EmployeeTypeId",
                table: "Employees",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "EmployeeTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeTypes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Employees_BossId",
                table: "Employees",
                column: "BossId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_EmployeeTypeId",
                table: "Employees",
                column: "EmployeeTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_EmployeeTypes_EmployeeTypeId",
                table: "Employees",
                column: "EmployeeTypeId",
                principalTable: "EmployeeTypes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Employees_BossId",
                table: "Employees",
                column: "BossId",
                principalTable: "Employees",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_EmployeeTypes_EmployeeTypeId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Employees_BossId",
                table: "Employees");

            migrationBuilder.DropTable(
                name: "EmployeeTypes");

            migrationBuilder.DropIndex(
                name: "IX_Employees_BossId",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_EmployeeTypeId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "BossId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "EmployeeTypeId",
                table: "Employees");
        }
    }
}
