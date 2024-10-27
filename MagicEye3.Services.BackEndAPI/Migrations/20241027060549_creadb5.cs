using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MagicEye3.Services.BackEndAPI.Migrations
{
    /// <inheritdoc />
    public partial class creadb5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Actividades_Evaluaciones_EvaluacionId",
                table: "Actividades");

            migrationBuilder.DropForeignKey(
                name: "FK_ComponenteActividades_Actividades_ActividadId",
                table: "ComponenteActividades");

            migrationBuilder.DropForeignKey(
                name: "FK_ComponenteActividades_Componentes_ComponenteId",
                table: "ComponenteActividades");

            migrationBuilder.DropForeignKey(
                name: "FK_ContenidoComponentes_Componentes_ComponenteId",
                table: "ContenidoComponentes");

            migrationBuilder.DropForeignKey(
                name: "FK_ContenidoComponentes_Contenidos_ContenidoId",
                table: "ContenidoComponentes");

            migrationBuilder.DropForeignKey(
                name: "FK_Contenidos_Unidades_UnidadId",
                table: "Contenidos");

            migrationBuilder.DropForeignKey(
                name: "FK_FechaContenidos_Contenidos_ContenidoId",
                table: "FechaContenidos");

            migrationBuilder.DropForeignKey(
                name: "FK_FechaContenidos_Fechas_FechaId",
                table: "FechaContenidos");

            migrationBuilder.DropForeignKey(
                name: "FK_Grupos_Periodos_PeriodoId",
                table: "Grupos");

            migrationBuilder.DropForeignKey(
                name: "FK_SilaboParciales_Parciales_ParcialId",
                table: "SilaboParciales");

            migrationBuilder.DropForeignKey(
                name: "FK_SilaboParciales_Silabos_SilaboId",
                table: "SilaboParciales");

            migrationBuilder.DropForeignKey(
                name: "FK_Silabos_Carreras_CarreraId",
                table: "Silabos");

            migrationBuilder.DropForeignKey(
                name: "FK_Silabos_Periodos_PeriodoId",
                table: "Silabos");

            migrationBuilder.DropForeignKey(
                name: "FK_Unidades_Silabos_SilaboId",
                table: "Unidades");

            migrationBuilder.AddForeignKey(
                name: "FK_Actividades_Evaluaciones_EvaluacionId",
                table: "Actividades",
                column: "EvaluacionId",
                principalTable: "Evaluaciones",
                principalColumn: "EvaluacionId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ComponenteActividades_Actividades_ActividadId",
                table: "ComponenteActividades",
                column: "ActividadId",
                principalTable: "Actividades",
                principalColumn: "ActividadId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ComponenteActividades_Componentes_ComponenteId",
                table: "ComponenteActividades",
                column: "ComponenteId",
                principalTable: "Componentes",
                principalColumn: "ComponenteId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ContenidoComponentes_Componentes_ComponenteId",
                table: "ContenidoComponentes",
                column: "ComponenteId",
                principalTable: "Componentes",
                principalColumn: "ComponenteId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ContenidoComponentes_Contenidos_ContenidoId",
                table: "ContenidoComponentes",
                column: "ContenidoId",
                principalTable: "Contenidos",
                principalColumn: "ContenidoId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Contenidos_Unidades_UnidadId",
                table: "Contenidos",
                column: "UnidadId",
                principalTable: "Unidades",
                principalColumn: "UnidadId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FechaContenidos_Contenidos_ContenidoId",
                table: "FechaContenidos",
                column: "ContenidoId",
                principalTable: "Contenidos",
                principalColumn: "ContenidoId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FechaContenidos_Fechas_FechaId",
                table: "FechaContenidos",
                column: "FechaId",
                principalTable: "Fechas",
                principalColumn: "FechaId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Grupos_Periodos_PeriodoId",
                table: "Grupos",
                column: "PeriodoId",
                principalTable: "Periodos",
                principalColumn: "PeriodoId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SilaboParciales_Parciales_ParcialId",
                table: "SilaboParciales",
                column: "ParcialId",
                principalTable: "Parciales",
                principalColumn: "ParcialId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SilaboParciales_Silabos_SilaboId",
                table: "SilaboParciales",
                column: "SilaboId",
                principalTable: "Silabos",
                principalColumn: "SilaboId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Silabos_Carreras_CarreraId",
                table: "Silabos",
                column: "CarreraId",
                principalTable: "Carreras",
                principalColumn: "CarreraId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Silabos_Periodos_PeriodoId",
                table: "Silabos",
                column: "PeriodoId",
                principalTable: "Periodos",
                principalColumn: "PeriodoId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Unidades_Silabos_SilaboId",
                table: "Unidades",
                column: "SilaboId",
                principalTable: "Silabos",
                principalColumn: "SilaboId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Actividades_Evaluaciones_EvaluacionId",
                table: "Actividades");

            migrationBuilder.DropForeignKey(
                name: "FK_ComponenteActividades_Actividades_ActividadId",
                table: "ComponenteActividades");

            migrationBuilder.DropForeignKey(
                name: "FK_ComponenteActividades_Componentes_ComponenteId",
                table: "ComponenteActividades");

            migrationBuilder.DropForeignKey(
                name: "FK_ContenidoComponentes_Componentes_ComponenteId",
                table: "ContenidoComponentes");

            migrationBuilder.DropForeignKey(
                name: "FK_ContenidoComponentes_Contenidos_ContenidoId",
                table: "ContenidoComponentes");

            migrationBuilder.DropForeignKey(
                name: "FK_Contenidos_Unidades_UnidadId",
                table: "Contenidos");

            migrationBuilder.DropForeignKey(
                name: "FK_FechaContenidos_Contenidos_ContenidoId",
                table: "FechaContenidos");

            migrationBuilder.DropForeignKey(
                name: "FK_FechaContenidos_Fechas_FechaId",
                table: "FechaContenidos");

            migrationBuilder.DropForeignKey(
                name: "FK_Grupos_Periodos_PeriodoId",
                table: "Grupos");

            migrationBuilder.DropForeignKey(
                name: "FK_SilaboParciales_Parciales_ParcialId",
                table: "SilaboParciales");

            migrationBuilder.DropForeignKey(
                name: "FK_SilaboParciales_Silabos_SilaboId",
                table: "SilaboParciales");

            migrationBuilder.DropForeignKey(
                name: "FK_Silabos_Carreras_CarreraId",
                table: "Silabos");

            migrationBuilder.DropForeignKey(
                name: "FK_Silabos_Periodos_PeriodoId",
                table: "Silabos");

            migrationBuilder.DropForeignKey(
                name: "FK_Unidades_Silabos_SilaboId",
                table: "Unidades");

            migrationBuilder.AddForeignKey(
                name: "FK_Actividades_Evaluaciones_EvaluacionId",
                table: "Actividades",
                column: "EvaluacionId",
                principalTable: "Evaluaciones",
                principalColumn: "EvaluacionId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ComponenteActividades_Actividades_ActividadId",
                table: "ComponenteActividades",
                column: "ActividadId",
                principalTable: "Actividades",
                principalColumn: "ActividadId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ComponenteActividades_Componentes_ComponenteId",
                table: "ComponenteActividades",
                column: "ComponenteId",
                principalTable: "Componentes",
                principalColumn: "ComponenteId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ContenidoComponentes_Componentes_ComponenteId",
                table: "ContenidoComponentes",
                column: "ComponenteId",
                principalTable: "Componentes",
                principalColumn: "ComponenteId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ContenidoComponentes_Contenidos_ContenidoId",
                table: "ContenidoComponentes",
                column: "ContenidoId",
                principalTable: "Contenidos",
                principalColumn: "ContenidoId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Contenidos_Unidades_UnidadId",
                table: "Contenidos",
                column: "UnidadId",
                principalTable: "Unidades",
                principalColumn: "UnidadId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FechaContenidos_Contenidos_ContenidoId",
                table: "FechaContenidos",
                column: "ContenidoId",
                principalTable: "Contenidos",
                principalColumn: "ContenidoId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FechaContenidos_Fechas_FechaId",
                table: "FechaContenidos",
                column: "FechaId",
                principalTable: "Fechas",
                principalColumn: "FechaId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Grupos_Periodos_PeriodoId",
                table: "Grupos",
                column: "PeriodoId",
                principalTable: "Periodos",
                principalColumn: "PeriodoId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SilaboParciales_Parciales_ParcialId",
                table: "SilaboParciales",
                column: "ParcialId",
                principalTable: "Parciales",
                principalColumn: "ParcialId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SilaboParciales_Silabos_SilaboId",
                table: "SilaboParciales",
                column: "SilaboId",
                principalTable: "Silabos",
                principalColumn: "SilaboId",
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
    }
}
