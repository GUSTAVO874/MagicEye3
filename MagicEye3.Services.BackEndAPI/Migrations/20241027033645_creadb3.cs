using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MagicEye3.Services.BackEndAPI.Migrations
{
    /// <inheritdoc />
    public partial class creadb3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Carreras",
                columns: table => new
                {
                    CarreraId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carreras", x => x.CarreraId);
                });

            migrationBuilder.CreateTable(
                name: "Componentes",
                columns: table => new
                {
                    ComponenteId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Componentes", x => x.ComponenteId);
                });

            migrationBuilder.CreateTable(
                name: "Evaluaciones",
                columns: table => new
                {
                    EvaluacionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Evaluaciones", x => x.EvaluacionId);
                });

            migrationBuilder.CreateTable(
                name: "Fechas",
                columns: table => new
                {
                    FechaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    laFecha = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fechas", x => x.FechaId);
                });

            migrationBuilder.CreateTable(
                name: "Parciales",
                columns: table => new
                {
                    ParcialId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parciales", x => x.ParcialId);
                });

            migrationBuilder.CreateTable(
                name: "Periodos",
                columns: table => new
                {
                    PeriodoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Periodos", x => x.PeriodoId);
                });

            migrationBuilder.CreateTable(
                name: "Actividades",
                columns: table => new
                {
                    ActividadId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EvaluacionId = table.Column<int>(type: "int", nullable: false),
                    Tiempo = table.Column<int>(type: "int", nullable: false),
                    EvaluacionId1 = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Actividades", x => x.ActividadId);
                    table.ForeignKey(
                        name: "FK_Actividades_Evaluaciones_EvaluacionId",
                        column: x => x.EvaluacionId,
                        principalTable: "Evaluaciones",
                        principalColumn: "EvaluacionId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Actividades_Evaluaciones_EvaluacionId1",
                        column: x => x.EvaluacionId1,
                        principalTable: "Evaluaciones",
                        principalColumn: "EvaluacionId");
                });

            migrationBuilder.CreateTable(
                name: "Grupos",
                columns: table => new
                {
                    GrupoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PeriodoId = table.Column<int>(type: "int", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Grupos", x => x.GrupoId);
                    table.ForeignKey(
                        name: "FK_Grupos_Periodos_PeriodoId",
                        column: x => x.PeriodoId,
                        principalTable: "Periodos",
                        principalColumn: "PeriodoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Silabos",
                columns: table => new
                {
                    SilaboId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CarreraId = table.Column<int>(type: "int", nullable: false),
                    PeriodoId = table.Column<int>(type: "int", nullable: false),
                    ParcialId = table.Column<int>(type: "int", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CarreraId1 = table.Column<int>(type: "int", nullable: true),
                    PeriodoId1 = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Silabos", x => x.SilaboId);
                    table.ForeignKey(
                        name: "FK_Silabos_Carreras_CarreraId",
                        column: x => x.CarreraId,
                        principalTable: "Carreras",
                        principalColumn: "CarreraId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Silabos_Carreras_CarreraId1",
                        column: x => x.CarreraId1,
                        principalTable: "Carreras",
                        principalColumn: "CarreraId");
                    table.ForeignKey(
                        name: "FK_Silabos_Parciales_ParcialId",
                        column: x => x.ParcialId,
                        principalTable: "Parciales",
                        principalColumn: "ParcialId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Silabos_Periodos_PeriodoId",
                        column: x => x.PeriodoId,
                        principalTable: "Periodos",
                        principalColumn: "PeriodoId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Silabos_Periodos_PeriodoId1",
                        column: x => x.PeriodoId1,
                        principalTable: "Periodos",
                        principalColumn: "PeriodoId");
                });

            migrationBuilder.CreateTable(
                name: "ComponenteActividades",
                columns: table => new
                {
                    ComponenteId = table.Column<int>(type: "int", nullable: false),
                    ActividadId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComponenteActividades", x => new { x.ComponenteId, x.ActividadId });
                    table.ForeignKey(
                        name: "FK_ComponenteActividades_Actividades_ActividadId",
                        column: x => x.ActividadId,
                        principalTable: "Actividades",
                        principalColumn: "ActividadId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ComponenteActividades_Componentes_ComponenteId",
                        column: x => x.ComponenteId,
                        principalTable: "Componentes",
                        principalColumn: "ComponenteId",
                        onDelete: ReferentialAction.Cascade);
                });

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
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SilaboParciales_Silabos_SilaboId",
                        column: x => x.SilaboId,
                        principalTable: "Silabos",
                        principalColumn: "SilaboId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Unidades",
                columns: table => new
                {
                    UnidadId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SilaboId = table.Column<int>(type: "int", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Unidades", x => x.UnidadId);
                    table.ForeignKey(
                        name: "FK_Unidades_Silabos_SilaboId",
                        column: x => x.SilaboId,
                        principalTable: "Silabos",
                        principalColumn: "SilaboId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Contenidos",
                columns: table => new
                {
                    ContenidoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UnidadId = table.Column<int>(type: "int", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contenidos", x => x.ContenidoId);
                    table.ForeignKey(
                        name: "FK_Contenidos_Unidades_UnidadId",
                        column: x => x.UnidadId,
                        principalTable: "Unidades",
                        principalColumn: "UnidadId",
                        onDelete: ReferentialAction.Cascade);
                });

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
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ContenidoComponentes_Contenidos_ContenidoId",
                        column: x => x.ContenidoId,
                        principalTable: "Contenidos",
                        principalColumn: "ContenidoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FechaContenidos",
                columns: table => new
                {
                    ContenidoId = table.Column<int>(type: "int", nullable: false),
                    FechaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FechaContenidos", x => new { x.ContenidoId, x.FechaId });
                    table.ForeignKey(
                        name: "FK_FechaContenidos_Contenidos_ContenidoId",
                        column: x => x.ContenidoId,
                        principalTable: "Contenidos",
                        principalColumn: "ContenidoId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FechaContenidos_Fechas_FechaId",
                        column: x => x.FechaId,
                        principalTable: "Fechas",
                        principalColumn: "FechaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Actividades_EvaluacionId",
                table: "Actividades",
                column: "EvaluacionId");

            migrationBuilder.CreateIndex(
                name: "IX_Actividades_EvaluacionId1",
                table: "Actividades",
                column: "EvaluacionId1");

            migrationBuilder.CreateIndex(
                name: "IX_ComponenteActividades_ActividadId",
                table: "ComponenteActividades",
                column: "ActividadId");

            migrationBuilder.CreateIndex(
                name: "IX_ContenidoComponentes_ComponenteId",
                table: "ContenidoComponentes",
                column: "ComponenteId");

            migrationBuilder.CreateIndex(
                name: "IX_Contenidos_UnidadId",
                table: "Contenidos",
                column: "UnidadId");

            migrationBuilder.CreateIndex(
                name: "IX_FechaContenidos_FechaId",
                table: "FechaContenidos",
                column: "FechaId");

            migrationBuilder.CreateIndex(
                name: "IX_Grupos_PeriodoId",
                table: "Grupos",
                column: "PeriodoId");

            migrationBuilder.CreateIndex(
                name: "IX_SilaboParciales_ParcialId",
                table: "SilaboParciales",
                column: "ParcialId");

            migrationBuilder.CreateIndex(
                name: "IX_Silabos_CarreraId",
                table: "Silabos",
                column: "CarreraId");

            migrationBuilder.CreateIndex(
                name: "IX_Silabos_CarreraId1",
                table: "Silabos",
                column: "CarreraId1");

            migrationBuilder.CreateIndex(
                name: "IX_Silabos_ParcialId",
                table: "Silabos",
                column: "ParcialId");

            migrationBuilder.CreateIndex(
                name: "IX_Silabos_PeriodoId",
                table: "Silabos",
                column: "PeriodoId");

            migrationBuilder.CreateIndex(
                name: "IX_Silabos_PeriodoId1",
                table: "Silabos",
                column: "PeriodoId1");

            migrationBuilder.CreateIndex(
                name: "IX_Unidades_SilaboId",
                table: "Unidades",
                column: "SilaboId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ComponenteActividades");

            migrationBuilder.DropTable(
                name: "ContenidoComponentes");

            migrationBuilder.DropTable(
                name: "FechaContenidos");

            migrationBuilder.DropTable(
                name: "Grupos");

            migrationBuilder.DropTable(
                name: "SilaboParciales");

            migrationBuilder.DropTable(
                name: "Actividades");

            migrationBuilder.DropTable(
                name: "Componentes");

            migrationBuilder.DropTable(
                name: "Contenidos");

            migrationBuilder.DropTable(
                name: "Fechas");

            migrationBuilder.DropTable(
                name: "Evaluaciones");

            migrationBuilder.DropTable(
                name: "Unidades");

            migrationBuilder.DropTable(
                name: "Silabos");

            migrationBuilder.DropTable(
                name: "Carreras");

            migrationBuilder.DropTable(
                name: "Parciales");

            migrationBuilder.DropTable(
                name: "Periodos");
        }
    }
}
