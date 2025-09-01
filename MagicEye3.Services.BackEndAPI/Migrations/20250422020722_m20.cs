using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MagicEye3.Services.BackEndAPI.Migrations
{
    /// <inheritdoc />
    public partial class m20 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Tiempoenminutos",
                table: "SilaboComponentes",
                newName: "Tiempo");

            migrationBuilder.AddColumn<int>(
                name: "HoraClase",
                table: "SilaboComponentes",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HoraClase",
                table: "SilaboComponentes");

            migrationBuilder.RenameColumn(
                name: "Tiempo",
                table: "SilaboComponentes",
                newName: "Tiempoenminutos");
        }
    }
}
