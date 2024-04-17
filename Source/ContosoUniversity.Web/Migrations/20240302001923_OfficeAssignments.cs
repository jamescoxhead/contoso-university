using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ContosoUniversity.Web.Migrations;

/// <inheritdoc />
public partial class OfficeAssignments : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder) => migrationBuilder.CreateTable(
            name: "OfficeAssignments",
            columns: table => new
            {
                InstructorId = table.Column<int>(type: "int", nullable: false),
                Location = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_OfficeAssignments", x => x.InstructorId);
                table.ForeignKey(
                    name: "FK_OfficeAssignments_Instructors_InstructorId",
                    column: x => x.InstructorId,
                    principalTable: "Instructors",
                    principalColumn: "InstructorId",
                    onDelete: ReferentialAction.Cascade);
            });

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder) => migrationBuilder.DropTable(
            name: "OfficeAssignments");
}
