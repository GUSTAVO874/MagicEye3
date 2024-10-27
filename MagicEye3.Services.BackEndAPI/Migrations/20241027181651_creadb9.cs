using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MagicEye3.Services.BackEndAPI.Migrations
{
    /// <inheritdoc />
    public partial class creadb9 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Grupos_Periodos_PeriodoId",
                table: "Grupos");

            migrationBuilder.DropForeignKey(
                name: "FK_Silabos_Periodos_PeriodoId",
                table: "Silabos");

            migrationBuilder.DropTable(
                name: "SilaboParciales");

            migrationBuilder.DropIndex(
                name: "IX_Grupos_PeriodoId",
                table: "Grupos");

            migrationBuilder.DropColumn(
                name: "PeriodoId",
                table: "Grupos");

            migrationBuilder.RenameColumn(
                name: "PeriodoId",
                table: "Silabos",
                newName: "CicloId");

            migrationBuilder.RenameIndex(
                name: "IX_Silabos_PeriodoId",
                table: "Silabos",
                newName: "IX_Silabos_CicloId");

            migrationBuilder.AddColumn<int>(
                name: "CicloId",
                table: "Parciales",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Ciclos",
                columns: table => new
                {
                    CicloId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Identificacion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PeriodoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ciclos", x => x.CicloId);
                    table.ForeignKey(
                        name: "FK_Ciclos_Periodos_PeriodoId",
                        column: x => x.PeriodoId,
                        principalTable: "Periodos",
                        principalColumn: "PeriodoId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SilaboGrupos",
                columns: table => new
                {
                    SilaboId = table.Column<int>(type: "int", nullable: false),
                    GrupoId = table.Column<int>(type: "int", nullable: false),
                    Dia = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HoraInicio = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HoraFin = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SilaboGrupos", x => new { x.SilaboId, x.GrupoId });
                    table.ForeignKey(
                        name: "FK_SilaboGrupos_Grupos_GrupoId",
                        column: x => x.GrupoId,
                        principalTable: "Grupos",
                        principalColumn: "GrupoId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SilaboGrupos_Silabos_SilaboId",
                        column: x => x.SilaboId,
                        principalTable: "Silabos",
                        principalColumn: "SilaboId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Parciales_CicloId",
                table: "Parciales",
                column: "CicloId");

            migrationBuilder.CreateIndex(
                name: "IX_Ciclos_PeriodoId",
                table: "Ciclos",
                column: "PeriodoId");

            migrationBuilder.CreateIndex(
                name: "IX_SilaboGrupos_GrupoId",
                table: "SilaboGrupos",
                column: "GrupoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Parciales_Ciclos_CicloId",
                table: "Parciales",
                column: "CicloId",
                principalTable: "Ciclos",
                principalColumn: "CicloId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Silabos_Ciclos_CicloId",
                table: "Silabos",
                column: "CicloId",
                principalTable: "Ciclos",
                principalColumn: "CicloId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Parciales_Ciclos_CicloId",
                table: "Parciales");

            migrationBuilder.DropForeignKey(
                name: "FK_Silabos_Ciclos_CicloId",
                table: "Silabos");

            migrationBuilder.DropTable(
                name: "Ciclos");

            migrationBuilder.DropTable(
                name: "SilaboGrupos");

            migrationBuilder.DropIndex(
                name: "IX_Parciales_CicloId",
                table: "Parciales");

            migrationBuilder.DropColumn(
                name: "CicloId",
                table: "Parciales");

            migrationBuilder.RenameColumn(
                name: "CicloId",
                table: "Silabos",
                newName: "PeriodoId");

            migrationBuilder.RenameIndex(
                name: "IX_Silabos_CicloId",
                table: "Silabos",
                newName: "IX_Silabos_PeriodoId");

            migrationBuilder.AddColumn<int>(
                name: "PeriodoId",
                table: "Grupos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "SilaboParciales",
                columns: table => new
                {
                    SilaboId = table.Column<int>(type: "int", nullable: false),
                    ParcialId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SilaboParciales", x => new { x.SilaboId, x.ParcialId });
                    table.ForeignKey(
                        name: "FK_SilaboParciales_Parciales_ParcialId",
                        column: x => x.ParcialId,
                        principalTable: "Parciales",
                        principalColumn: "ParcialId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SilaboParciales_Silabos_SilaboId",
                        column: x => x.SilaboId,
                        principalTable: "Silabos",
                        principalColumn: "SilaboId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Grupos_PeriodoId",
                table: "Grupos",
                column: "PeriodoId");

            migrationBuilder.CreateIndex(
                name: "IX_SilaboParciales_ParcialId",
                table: "SilaboParciales",
                column: "ParcialId");

            migrationBuilder.AddForeignKey(
                name: "FK_Grupos_Periodos_PeriodoId",
                table: "Grupos",
                column: "PeriodoId",
                principalTable: "Periodos",
                principalColumn: "PeriodoId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Silabos_Periodos_PeriodoId",
                table: "Silabos",
                column: "PeriodoId",
                principalTable: "Periodos",
                principalColumn: "PeriodoId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
