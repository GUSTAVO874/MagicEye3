using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MagicEye3.Services.BackEndAPI.Migrations
{
    /// <inheritdoc />
    public partial class creadb4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Actividades_Evaluaciones_EvaluacionId",
                table: "Actividades");

            migrationBuilder.DropForeignKey(
                name: "FK_Actividades_Evaluaciones_EvaluacionId1",
                table: "Actividades");

            migrationBuilder.DropForeignKey(
                name: "FK_Silabos_Carreras_CarreraId",
                table: "Silabos");

            migrationBuilder.DropForeignKey(
                name: "FK_Silabos_Carreras_CarreraId1",
                table: "Silabos");

            migrationBuilder.DropForeignKey(
                name: "FK_Silabos_Parciales_ParcialId",
                table: "Silabos");

            migrationBuilder.DropForeignKey(
                name: "FK_Silabos_Periodos_PeriodoId",
                table: "Silabos");

            migrationBuilder.DropForeignKey(
                name: "FK_Silabos_Periodos_PeriodoId1",
                table: "Silabos");

            migrationBuilder.DropForeignKey(
                name: "FK_Unidades_Silabos_SilaboId",
                table: "Unidades");

            migrationBuilder.DropIndex(
                name: "IX_Silabos_CarreraId1",
                table: "Silabos");

            migrationBuilder.DropIndex(
                name: "IX_Silabos_ParcialId",
                table: "Silabos");

            migrationBuilder.DropIndex(
                name: "IX_Silabos_PeriodoId1",
                table: "Silabos");

            migrationBuilder.DropIndex(
                name: "IX_Actividades_EvaluacionId1",
                table: "Actividades");

            migrationBuilder.DropColumn(
                name: "CarreraId1",
                table: "Silabos");

            migrationBuilder.DropColumn(
                name: "ParcialId",
                table: "Silabos");

            migrationBuilder.DropColumn(
                name: "PeriodoId1",
                table: "Silabos");

            migrationBuilder.DropColumn(
                name: "EvaluacionId1",
                table: "Actividades");

            migrationBuilder.AddForeignKey(
                name: "FK_Actividades_Evaluaciones_EvaluacionId",
                table: "Actividades",
                column: "EvaluacionId",
                principalTable: "Evaluaciones",
                principalColumn: "EvaluacionId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Silabos_Carreras_CarreraId",
                table: "Silabos",
                column: "CarreraId",
                principalTable: "Carreras",
                principalColumn: "CarreraId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Silabos_Periodos_PeriodoId",
                table: "Silabos",
                column: "PeriodoId",
                principalTable: "Periodos",
                principalColumn: "PeriodoId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Unidades_Silabos_SilaboId",
                table: "Unidades",
                column: "SilaboId",
                principalTable: "Silabos",
                principalColumn: "SilaboId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Actividades_Evaluaciones_EvaluacionId",
                table: "Actividades");

            migrationBuilder.DropForeignKey(
                name: "FK_Silabos_Carreras_CarreraId",
                table: "Silabos");

            migrationBuilder.DropForeignKey(
                name: "FK_Silabos_Periodos_PeriodoId",
                table: "Silabos");

            migrationBuilder.DropForeignKey(
                name: "FK_Unidades_Silabos_SilaboId",
                table: "Unidades");

            migrationBuilder.AddColumn<int>(
                name: "CarreraId1",
                table: "Silabos",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ParcialId",
                table: "Silabos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PeriodoId1",
                table: "Silabos",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EvaluacionId1",
                table: "Actividades",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Silabos_CarreraId1",
                table: "Silabos",
                column: "CarreraId1");

            migrationBuilder.CreateIndex(
                name: "IX_Silabos_ParcialId",
                table: "Silabos",
                column: "ParcialId");

            migrationBuilder.CreateIndex(
                name: "IX_Silabos_PeriodoId1",
                table: "Silabos",
                column: "PeriodoId1");

            migrationBuilder.CreateIndex(
                name: "IX_Actividades_EvaluacionId1",
                table: "Actividades",
                column: "EvaluacionId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Actividades_Evaluaciones_EvaluacionId",
                table: "Actividades",
                column: "EvaluacionId",
                principalTable: "Evaluaciones",
                principalColumn: "EvaluacionId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Actividades_Evaluaciones_EvaluacionId1",
                table: "Actividades",
                column: "EvaluacionId1",
                principalTable: "Evaluaciones",
                principalColumn: "EvaluacionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Silabos_Carreras_CarreraId",
                table: "Silabos",
                column: "CarreraId",
                principalTable: "Carreras",
                principalColumn: "CarreraId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Silabos_Carreras_CarreraId1",
                table: "Silabos",
                column: "CarreraId1",
                principalTable: "Carreras",
                principalColumn: "CarreraId");

            migrationBuilder.AddForeignKey(
                name: "FK_Silabos_Parciales_ParcialId",
                table: "Silabos",
                column: "ParcialId",
                principalTable: "Parciales",
                principalColumn: "ParcialId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Silabos_Periodos_PeriodoId",
                table: "Silabos",
                column: "PeriodoId",
                principalTable: "Periodos",
                principalColumn: "PeriodoId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Silabos_Periodos_PeriodoId1",
                table: "Silabos",
                column: "PeriodoId1",
                principalTable: "Periodos",
                principalColumn: "PeriodoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Unidades_Silabos_SilaboId",
                table: "Unidades",
                column: "SilaboId",
                principalTable: "Silabos",
                principalColumn: "SilaboId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
