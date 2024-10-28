using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MagicEye3.Services.BackEndAPI.Migrations
{
    /// <inheritdoc />
    public partial class creadb10 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Silabos_Carreras_CarreraId",
                table: "Silabos");

            migrationBuilder.DropIndex(
                name: "IX_Silabos_CarreraId",
                table: "Silabos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FechaContenidos",
                table: "FechaContenidos");

            migrationBuilder.DropIndex(
                name: "IX_FechaContenidos_FechaId",
                table: "FechaContenidos");

            migrationBuilder.DropColumn(
                name: "CarreraId",
                table: "Silabos");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FechaContenidos",
                table: "FechaContenidos",
                columns: new[] { "FechaId", "ContenidoId" });

            migrationBuilder.CreateTable(
                name: "CarreraPeriodos",
                columns: table => new
                {
                    CarreraId = table.Column<int>(type: "int", nullable: false),
                    PeriodoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarreraPeriodos", x => new { x.CarreraId, x.PeriodoId });
                    table.ForeignKey(
                        name: "FK_CarreraPeriodos_Carreras_CarreraId",
                        column: x => x.CarreraId,
                        principalTable: "Carreras",
                        principalColumn: "CarreraId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CarreraPeriodos_Periodos_PeriodoId",
                        column: x => x.PeriodoId,
                        principalTable: "Periodos",
                        principalColumn: "PeriodoId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FechaContenidos_ContenidoId",
                table: "FechaContenidos",
                column: "ContenidoId");

            migrationBuilder.CreateIndex(
                name: "IX_CarreraPeriodos_PeriodoId",
                table: "CarreraPeriodos",
                column: "PeriodoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CarreraPeriodos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FechaContenidos",
                table: "FechaContenidos");

            migrationBuilder.DropIndex(
                name: "IX_FechaContenidos_ContenidoId",
                table: "FechaContenidos");

            migrationBuilder.AddColumn<int>(
                name: "CarreraId",
                table: "Silabos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_FechaContenidos",
                table: "FechaContenidos",
                columns: new[] { "ContenidoId", "FechaId" });

            migrationBuilder.CreateIndex(
                name: "IX_Silabos_CarreraId",
                table: "Silabos",
                column: "CarreraId");

            migrationBuilder.CreateIndex(
                name: "IX_FechaContenidos_FechaId",
                table: "FechaContenidos",
                column: "FechaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Silabos_Carreras_CarreraId",
                table: "Silabos",
                column: "CarreraId",
                principalTable: "Carreras",
                principalColumn: "CarreraId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
