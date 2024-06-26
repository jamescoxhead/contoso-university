using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ContosoUniversity.Web.Migrations;

/// <inheritdoc />
public partial class DepartmentRowVersion : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder) => migrationBuilder.AddColumn<byte[]>(
            name: "RowVersion",
            table: "Departments",
            type: "rowversion",
            rowVersion: true,
            nullable: false,
            defaultValue: Array.Empty<byte>());

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder) => migrationBuilder.DropColumn(
            name: "RowVersion",
            table: "Departments");
}
