using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MagicEye3.Services.BackEndAPI.Migrations
{
    /// <inheritdoc />
    public partial class connuevarelacion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContenidoComponentes");

            migrationBuilder.CreateTable(
                name: "ContenidoActividades",
                columns: table => new
                {
                    ContenidoId = table.Column<int>(type: "int", nullable: false),
                    ActividadId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContenidoActividades", x => new { x.ContenidoId, x.ActividadId });
                    table.ForeignKey(
                        name: "FK_ContenidoActividades_Actividades_ActividadId",
                        column: x => x.ActividadId,
                        principalTable: "Actividades",
                        principalColumn: "ActividadId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ContenidoActividades_Contenidos_ContenidoId",
                        column: x => x.ContenidoId,
                        principalTable: "Contenidos",
                        principalColumn: "ContenidoId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ContenidoActividades_ActividadId",
                table: "ContenidoActividades",
                column: "ActividadId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContenidoActividades");

            migrationBuilder.CreateTable(
                name: "ContenidoComponentes",
                columns: table => new
                {
                    ContenidoId = table.Column<int>(type: "int", nullable: false),
                    ComponenteId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContenidoComponentes", x => new { x.ContenidoId, x.ComponenteId });
                    table.ForeignKey(
                        name: "FK_ContenidoComponentes_Componentes_ComponenteId",
                        column: x => x.ComponenteId,
                        principalTable: "Componentes",
                        principalColumn: "ComponenteId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ContenidoComponentes_Contenidos_ContenidoId",
                        column: x => x.ContenidoId,
                        principalTable: "Contenidos",
                        principalColumn: "ContenidoId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ContenidoComponentes_ComponenteId",
                table: "ContenidoComponentes",
                column: "ComponenteId");
        }
    }
}
