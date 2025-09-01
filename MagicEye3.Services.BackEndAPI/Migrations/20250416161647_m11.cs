using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MagicEye3.Services.BackEndAPI.Migrations
{
    /// <inheritdoc />
    public partial class m11 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DiaLabor",
                table: "Calendarios");

            migrationBuilder.DropColumn(
                name: "Examen",
                table: "Calendarios");

            migrationBuilder.AddColumn<bool>(
                name: "Examen",
                table: "AsignaturaCalendarios",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Examen",
                table: "AsignaturaCalendarios");

            migrationBuilder.AddColumn<bool>(
                name: "DiaLabor",
                table: "Calendarios",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Examen",
                table: "Calendarios",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
