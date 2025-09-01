using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MagicEye3.Services.BackEndAPI.Migrations
{
    /// <inheritdoc />
    public partial class m12 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Feriado",
                table: "Calendarios",
                newName: "SinClases");

            migrationBuilder.AddColumn<bool>(
                name: "FinClases",
                table: "AsignaturaCalendarios",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "InicioClases",
                table: "AsignaturaCalendarios",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FinClases",
                table: "AsignaturaCalendarios");

            migrationBuilder.DropColumn(
                name: "InicioClases",
                table: "AsignaturaCalendarios");

            migrationBuilder.RenameColumn(
                name: "SinClases",
                table: "Calendarios",
                newName: "Feriado");
        }
    }
}
