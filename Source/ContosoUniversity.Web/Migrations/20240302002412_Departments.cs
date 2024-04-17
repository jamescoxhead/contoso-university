using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ContosoUniversity.Web.Migrations;

/// <inheritdoc />
public partial class Departments : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<int>(
            name: "DepartmentId",
            table: "Courses",
            type: "int",
            nullable: false,
            defaultValue: 0);

        migrationBuilder.CreateTable(
            name: "Departments",
            columns: table => new
            {
                DepartmentId = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                Budget = table.Column<decimal>(type: "money", nullable: false),
                StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                InstructorId = table.Column<int>(type: "int", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Departments", x => x.DepartmentId);
                table.ForeignKey(
                    name: "FK_Departments_Instructors_InstructorId",
                    column: x => x.InstructorId,
                    principalTable: "Instructors",
                    principalColumn: "InstructorId");
            });

        migrationBuilder.CreateIndex(
            name: "IX_Courses_DepartmentId",
            table: "Courses",
            column: "DepartmentId");

        migrationBuilder.CreateIndex(
            name: "IX_Departments_InstructorId",
            table: "Departments",
            column: "InstructorId");

        migrationBuilder.AddForeignKey(
            name: "FK_Courses_Departments_DepartmentId",
            table: "Courses",
            column: "DepartmentId",
            principalTable: "Departments",
            principalColumn: "DepartmentId",
            onDelete: ReferentialAction.Cascade);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            name: "FK_Courses_Departments_DepartmentId",
            table: "Courses");

        migrationBuilder.DropTable(
            name: "Departments");

        migrationBuilder.DropIndex(
            name: "IX_Courses_DepartmentId",
            table: "Courses");

        migrationBuilder.DropColumn(
            name: "DepartmentId",
            table: "Courses");
    }
}
