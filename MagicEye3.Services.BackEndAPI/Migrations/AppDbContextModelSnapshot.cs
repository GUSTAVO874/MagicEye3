﻿// <auto-generated />
using System;
using MagicEye3.Services.BackEndAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace MagicEye3.Services.BackEndAPI.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("MagicEye3.Services.BackEndAPI.Models.Actividad", b =>
                {
                    b.Property<int>("ActividadId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ActividadId"));

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("EvaluacionId")
                        .HasColumnType("int");

                    b.Property<int>("Tiempo")
                        .HasColumnType("int");

                    b.HasKey("ActividadId");

                    b.HasIndex("EvaluacionId");

                    b.ToTable("Actividades");
                });

            modelBuilder.Entity("MagicEye3.Services.BackEndAPI.Models.Carrera", b =>
                {
                    b.Property<int>("CarreraId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CarreraId"));

                    b.Property<string>("Nombre")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CarreraId");

                    b.ToTable("Carreras");
                });

            modelBuilder.Entity("MagicEye3.Services.BackEndAPI.Models.CarreraPeriodo", b =>
                {
                    b.Property<int>("CarreraId")
                        .HasColumnType("int");

                    b.Property<int>("PeriodoId")
                        .HasColumnType("int");

                    b.HasKey("CarreraId", "PeriodoId");

                    b.HasIndex("PeriodoId");

                    b.ToTable("CarreraPeriodos");
                });

            modelBuilder.Entity("MagicEye3.Services.BackEndAPI.Models.Ciclo", b =>
                {
                    b.Property<int>("CicloId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CicloId"));

                    b.Property<string>("Identificacion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PeriodoId")
                        .HasColumnType("int");

                    b.HasKey("CicloId");

                    b.HasIndex("PeriodoId");

                    b.ToTable("Ciclos");
                });

            modelBuilder.Entity("MagicEye3.Services.BackEndAPI.Models.Componente", b =>
                {
                    b.Property<int>("ComponenteId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ComponenteId"));

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ComponenteId");

                    b.ToTable("Componentes");
                });

            modelBuilder.Entity("MagicEye3.Services.BackEndAPI.Models.ComponenteActividad", b =>
                {
                    b.Property<int>("ComponenteId")
                        .HasColumnType("int");

                    b.Property<int>("ActividadId")
                        .HasColumnType("int");

                    b.HasKey("ComponenteId", "ActividadId");

                    b.HasIndex("ActividadId");

                    b.ToTable("ComponenteActividades");
                });

            modelBuilder.Entity("MagicEye3.Services.BackEndAPI.Models.Contenido", b =>
                {
                    b.Property<int>("ContenidoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ContenidoId"));

                    b.Property<string>("Descripcion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UnidadId")
                        .HasColumnType("int");

                    b.HasKey("ContenidoId");

                    b.HasIndex("UnidadId");

                    b.ToTable("Contenidos");
                });

            modelBuilder.Entity("MagicEye3.Services.BackEndAPI.Models.ContenidoActividad", b =>
                {
                    b.Property<int>("ContenidoId")
                        .HasColumnType("int");

                    b.Property<int>("ActividadId")
                        .HasColumnType("int");

                    b.HasKey("ContenidoId", "ActividadId");

                    b.HasIndex("ActividadId");

                    b.ToTable("ContenidoActividades");
                });

            modelBuilder.Entity("MagicEye3.Services.BackEndAPI.Models.Evaluacion", b =>
                {
                    b.Property<int>("EvaluacionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("EvaluacionId"));

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("EvaluacionId");

                    b.ToTable("Evaluaciones");
                });

            modelBuilder.Entity("MagicEye3.Services.BackEndAPI.Models.Fecha", b =>
                {
                    b.Property<int>("FechaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("FechaId"));

                    b.Property<DateOnly>("laFecha")
                        .HasColumnType("date");

                    b.HasKey("FechaId");

                    b.ToTable("Fechas");
                });

            modelBuilder.Entity("MagicEye3.Services.BackEndAPI.Models.FechaContenido", b =>
                {
                    b.Property<int>("FechaId")
                        .HasColumnType("int");

                    b.Property<int>("ContenidoId")
                        .HasColumnType("int");

                    b.HasKey("FechaId", "ContenidoId");

                    b.HasIndex("ContenidoId");

                    b.ToTable("FechaContenidos");
                });

            modelBuilder.Entity("MagicEye3.Services.BackEndAPI.Models.Grupo", b =>
                {
                    b.Property<int>("GrupoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("GrupoId"));

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("GrupoId");

                    b.ToTable("Grupos");
                });

            modelBuilder.Entity("MagicEye3.Services.BackEndAPI.Models.Parcial", b =>
                {
                    b.Property<int>("ParcialId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ParcialId"));

                    b.Property<int>("CicloId")
                        .HasColumnType("int");

                    b.Property<string>("Nombre")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ParcialId");

                    b.HasIndex("CicloId");

                    b.ToTable("Parciales");
                });

            modelBuilder.Entity("MagicEye3.Services.BackEndAPI.Models.Periodo", b =>
                {
                    b.Property<int>("PeriodoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PeriodoId"));

                    b.Property<string>("Nombre")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PeriodoId");

                    b.ToTable("Periodos");
                });

            modelBuilder.Entity("MagicEye3.Services.BackEndAPI.Models.Silabo", b =>
                {
                    b.Property<int>("SilaboId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SilaboId"));

                    b.Property<int>("CicloId")
                        .HasColumnType("int");

                    b.Property<string>("Nombre")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("SilaboId");

                    b.HasIndex("CicloId");

                    b.ToTable("Silabos");
                });

            modelBuilder.Entity("MagicEye3.Services.BackEndAPI.Models.SilaboGrupo", b =>
                {
                    b.Property<int>("SilaboId")
                        .HasColumnType("int");

                    b.Property<int>("GrupoId")
                        .HasColumnType("int");

                    b.Property<string>("Dia")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HoraFin")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HoraInicio")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("SilaboId", "GrupoId");

                    b.HasIndex("GrupoId");

                    b.ToTable("SilaboGrupos");
                });

            modelBuilder.Entity("MagicEye3.Services.BackEndAPI.Models.Unidad", b =>
                {
                    b.Property<int>("UnidadId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UnidadId"));

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SilaboId")
                        .HasColumnType("int");

                    b.HasKey("UnidadId");

                    b.HasIndex("SilaboId");

                    b.ToTable("Unidades");
                });

            modelBuilder.Entity("MagicEye3.Services.BackEndAPI.Models.Actividad", b =>
                {
                    b.HasOne("MagicEye3.Services.BackEndAPI.Models.Evaluacion", "Evaluacion")
                        .WithMany("Actividades")
                        .HasForeignKey("EvaluacionId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Evaluacion");
                });

            modelBuilder.Entity("MagicEye3.Services.BackEndAPI.Models.CarreraPeriodo", b =>
                {
                    b.HasOne("MagicEye3.Services.BackEndAPI.Models.Carrera", "Carrera")
                        .WithMany("CarreraPeriodos")
                        .HasForeignKey("CarreraId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("MagicEye3.Services.BackEndAPI.Models.Periodo", "Periodo")
                        .WithMany("CarreraPeriodos")
                        .HasForeignKey("PeriodoId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Carrera");

                    b.Navigation("Periodo");
                });

            modelBuilder.Entity("MagicEye3.Services.BackEndAPI.Models.Ciclo", b =>
                {
                    b.HasOne("MagicEye3.Services.BackEndAPI.Models.Periodo", "Periodo")
                        .WithMany("Ciclos")
                        .HasForeignKey("PeriodoId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Periodo");
                });

            modelBuilder.Entity("MagicEye3.Services.BackEndAPI.Models.ComponenteActividad", b =>
                {
                    b.HasOne("MagicEye3.Services.BackEndAPI.Models.Actividad", "Actividad")
                        .WithMany("ComponenteActividades")
                        .HasForeignKey("ActividadId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("MagicEye3.Services.BackEndAPI.Models.Componente", "Componente")
                        .WithMany("ComponenteActividades")
                        .HasForeignKey("ComponenteId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Actividad");

                    b.Navigation("Componente");
                });

            modelBuilder.Entity("MagicEye3.Services.BackEndAPI.Models.Contenido", b =>
                {
                    b.HasOne("MagicEye3.Services.BackEndAPI.Models.Unidad", "Unidad")
                        .WithMany("Contenidos")
                        .HasForeignKey("UnidadId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Unidad");
                });

            modelBuilder.Entity("MagicEye3.Services.BackEndAPI.Models.ContenidoActividad", b =>
                {
                    b.HasOne("MagicEye3.Services.BackEndAPI.Models.Actividad", "Actividad")
                        .WithMany("ContenidoActividades")
                        .HasForeignKey("ActividadId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("MagicEye3.Services.BackEndAPI.Models.Contenido", "Contenido")
                        .WithMany("ContenidoActividades")
                        .HasForeignKey("ContenidoId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Actividad");

                    b.Navigation("Contenido");
                });

            modelBuilder.Entity("MagicEye3.Services.BackEndAPI.Models.FechaContenido", b =>
                {
                    b.HasOne("MagicEye3.Services.BackEndAPI.Models.Contenido", "Contenido")
                        .WithMany("FechaContenidos")
                        .HasForeignKey("ContenidoId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("MagicEye3.Services.BackEndAPI.Models.Fecha", "Fecha")
                        .WithMany("FechaContenidos")
                        .HasForeignKey("FechaId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Contenido");

                    b.Navigation("Fecha");
                });

            modelBuilder.Entity("MagicEye3.Services.BackEndAPI.Models.Parcial", b =>
                {
                    b.HasOne("MagicEye3.Services.BackEndAPI.Models.Ciclo", "Ciclo")
                        .WithMany("Parciales")
                        .HasForeignKey("CicloId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Ciclo");
                });

            modelBuilder.Entity("MagicEye3.Services.BackEndAPI.Models.Silabo", b =>
                {
                    b.HasOne("MagicEye3.Services.BackEndAPI.Models.Ciclo", "Ciclo")
                        .WithMany("Silabos")
                        .HasForeignKey("CicloId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Ciclo");
                });

            modelBuilder.Entity("MagicEye3.Services.BackEndAPI.Models.SilaboGrupo", b =>
                {
                    b.HasOne("MagicEye3.Services.BackEndAPI.Models.Grupo", "Grupo")
                        .WithMany("SilaboGrupos")
                        .HasForeignKey("GrupoId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("MagicEye3.Services.BackEndAPI.Models.Silabo", "Silabo")
                        .WithMany("SilaboGrupos")
                        .HasForeignKey("SilaboId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Grupo");

                    b.Navigation("Silabo");
                });

            modelBuilder.Entity("MagicEye3.Services.BackEndAPI.Models.Unidad", b =>
                {
                    b.HasOne("MagicEye3.Services.BackEndAPI.Models.Silabo", "Silabo")
                        .WithMany("Unidades")
                        .HasForeignKey("SilaboId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Silabo");
                });

            modelBuilder.Entity("MagicEye3.Services.BackEndAPI.Models.Actividad", b =>
                {
                    b.Navigation("ComponenteActividades");

                    b.Navigation("ContenidoActividades");
                });

            modelBuilder.Entity("MagicEye3.Services.BackEndAPI.Models.Carrera", b =>
                {
                    b.Navigation("CarreraPeriodos");
                });

            modelBuilder.Entity("MagicEye3.Services.BackEndAPI.Models.Ciclo", b =>
                {
                    b.Navigation("Parciales");

                    b.Navigation("Silabos");
                });

            modelBuilder.Entity("MagicEye3.Services.BackEndAPI.Models.Componente", b =>
                {
                    b.Navigation("ComponenteActividades");
                });

            modelBuilder.Entity("MagicEye3.Services.BackEndAPI.Models.Contenido", b =>
                {
                    b.Navigation("ContenidoActividades");

                    b.Navigation("FechaContenidos");
                });

            modelBuilder.Entity("MagicEye3.Services.BackEndAPI.Models.Evaluacion", b =>
                {
                    b.Navigation("Actividades");
                });

            modelBuilder.Entity("MagicEye3.Services.BackEndAPI.Models.Fecha", b =>
                {
                    b.Navigation("FechaContenidos");
                });

            modelBuilder.Entity("MagicEye3.Services.BackEndAPI.Models.Grupo", b =>
                {
                    b.Navigation("SilaboGrupos");
                });

            modelBuilder.Entity("MagicEye3.Services.BackEndAPI.Models.Periodo", b =>
                {
                    b.Navigation("CarreraPeriodos");

                    b.Navigation("Ciclos");
                });

            modelBuilder.Entity("MagicEye3.Services.BackEndAPI.Models.Silabo", b =>
                {
                    b.Navigation("SilaboGrupos");

                    b.Navigation("Unidades");
                });

            modelBuilder.Entity("MagicEye3.Services.BackEndAPI.Models.Unidad", b =>
                {
                    b.Navigation("Contenidos");
                });
#pragma warning restore 612, 618
        }
    }
}
