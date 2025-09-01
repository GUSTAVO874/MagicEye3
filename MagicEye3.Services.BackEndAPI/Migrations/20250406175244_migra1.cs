using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MagicEye3.Services.BackEndAPI.Migrations
{
    /// <inheritdoc />
    public partial class migra1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Nombre",
                table: "Silabos",
                newName: "Version");

            migrationBuilder.AddColumn<int>(
                name: "AsignaturaId",
                table: "Silabos",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "laFecha",
                table: "Fechas",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.CreateTable(
                name: "Asignaturas",
                columns: table => new
                {
                    AsignaturaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Asignaturas", x => x.AsignaturaId);
                });

            migrationBuilder.CreateTable(
                name: "Dias",
                columns: table => new
                {
                    DiaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dias", x => x.DiaId);
                });

            migrationBuilder.CreateTable(
                name: "AsignaturaDias",
                columns: table => new
                {
                    AsignaturaId = table.Column<int>(type: "int", nullable: false),
                    DiaId = table.Column<int>(type: "int", nullable: false),
                    HoraInicio = table.Column<TimeSpan>(type: "time", nullable: false),
                    HoraFin = table.Column<TimeSpan>(type: "time", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AsignaturaDias", x => new { x.AsignaturaId, x.DiaId, x.HoraInicio });
                    table.ForeignKey(
                        name: "FK_AsignaturaDias_Asignaturas_AsignaturaId",
                        column: x => x.AsignaturaId,
                        principalTable: "Asignaturas",
                        principalColumn: "AsignaturaId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AsignaturaDias_Dias_DiaId",
                        column: x => x.DiaId,
                        principalTable: "Dias",
                        principalColumn: "DiaId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Silabos_AsignaturaId",
                table: "Silabos",
                column: "AsignaturaId");

            migrationBuilder.CreateIndex(
                name: "IX_AsignaturaDias_DiaId",
                table: "AsignaturaDias",
                column: "DiaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Silabos_Asignaturas_AsignaturaId",
                table: "Silabos",
                column: "AsignaturaId",
                principalTable: "Asignaturas",
                principalColumn: "AsignaturaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Silabos_Asignaturas_AsignaturaId",
                table: "Silabos");

            migrationBuilder.DropTable(
                name: "AsignaturaDias");

            migrationBuilder.DropTable(
                name: "Asignaturas");

            migrationBuilder.DropTable(
                name: "Dias");

            migrationBuilder.DropIndex(
                name: "IX_Silabos_AsignaturaId",
                table: "Silabos");

            migrationBuilder.DropColumn(
                name: "AsignaturaId",
                table: "Silabos");

            migrationBuilder.RenameColumn(
                name: "Version",
                table: "Silabos",
                newName: "Nombre");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "laFecha",
                table: "Fechas",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");
        }
    }
}
