using Microsoft.EntityFrameworkCore;
using MagicEye3.Services.BackEndAPI.Models;

namespace MagicEye3.Services.BackEndAPI.Data
{
    public class AppDbContext : DbContext
    {
        // Constructor
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        // DbSets for your entities
        public DbSet<Actividad> Actividades { get; set; }
        public DbSet<Carrera> Carreras { get; set; }
        public DbSet<Ciclo> Ciclos { get; set; }
        public DbSet<Componente> Componentes { get; set; }
        public DbSet<ComponenteActividad> ComponenteActividades { get; set; }
        public DbSet<Contenido> Contenidos { get; set; }
        public DbSet<ContenidoComponente> ContenidoComponentes { get; set; }
        public DbSet<Evaluacion> Evaluaciones { get; set; }
        public DbSet<Fecha> Fechas { get; set; }
        public DbSet<FechaContenido> FechaContenidos { get; set; }
        public DbSet<Grupo> Grupos { get; set; }
        public DbSet<Parcial> Parciales { get; set; }
        public DbSet<Periodo> Periodos { get; set; }
        public DbSet<Silabo> Silabos { get; set; }
        public DbSet<SilaboGrupo> SilaboGrupos { get; set; }
        public DbSet<Unidad> Unidades { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Many-to-Many: ComponenteActividad
            modelBuilder.Entity<ComponenteActividad>()
                .HasKey(ca => new { ca.ComponenteId, ca.ActividadId });

            modelBuilder.Entity<ComponenteActividad>()
                .HasOne(ca => ca.Componente)
                .WithMany(c => c.ComponenteActividades)
                .HasForeignKey(ca => ca.ComponenteId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ComponenteActividad>()
                .HasOne(ca => ca.Actividad)
                .WithMany(a => a.ComponenteActividades)
                .HasForeignKey(ca => ca.ActividadId)
                .OnDelete(DeleteBehavior.Restrict);

            // Many-to-Many: ContenidoComponente
            modelBuilder.Entity<ContenidoComponente>()
                .HasKey(cc => new { cc.ContenidoId, cc.ComponenteId });

            modelBuilder.Entity<ContenidoComponente>()
                .HasOne(cc => cc.Contenido)
                .WithMany(c => c.ContenidoComponentes)
                .HasForeignKey(cc => cc.ContenidoId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ContenidoComponente>()
                .HasOne(cc => cc.Componente)
                .WithMany(c => c.ContenidoComponentes)
                .HasForeignKey(cc => cc.ComponenteId)
                .OnDelete(DeleteBehavior.Restrict);

            // Many-to-Many: FechaContenido
            modelBuilder.Entity<FechaContenido>()
                .HasKey(fc => new { fc.ContenidoId, fc.FechaId });

            modelBuilder.Entity<FechaContenido>()
                .HasOne(fc => fc.Contenido)
                .WithMany(c => c.FechaContenidos)
                .HasForeignKey(fc => fc.ContenidoId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<FechaContenido>()
                .HasOne(fc => fc.Fecha)
                .WithMany(f => f.FechaContenidos)
                .HasForeignKey(fc => fc.FechaId)
                .OnDelete(DeleteBehavior.Restrict);

            // Many-to-Many: SilaboGrupo
            modelBuilder.Entity<SilaboGrupo>()
                .HasKey(sg => new { sg.SilaboId, sg.GrupoId });

            modelBuilder.Entity<SilaboGrupo>()
                .HasOne(sg => sg.Silabo)
                .WithMany(s => s.SilaboGrupos)
                .HasForeignKey(sg => sg.SilaboId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<SilaboGrupo>()
                .HasOne(sg => sg.Grupo)
                .WithMany(g => g.SilaboGrupos)
                .HasForeignKey(sg => sg.GrupoId)
                .OnDelete(DeleteBehavior.Restrict);

            // One-to-Many: Actividad - Evaluacion
            modelBuilder.Entity<Actividad>()
                .HasOne(a => a.Evaluacion)
                .WithMany(e => e.Actividades)
                .HasForeignKey(a => a.EvaluacionId)
                .OnDelete(DeleteBehavior.Restrict);

            // One-to-Many: Contenido - Unidad
            modelBuilder.Entity<Contenido>()
                .HasOne(c => c.Unidad)
                .WithMany(u => u.Contenidos)
                .HasForeignKey(c => c.UnidadId)
                .OnDelete(DeleteBehavior.Restrict);

            // One-to-Many: Unidad - Silabo
            modelBuilder.Entity<Unidad>()
                .HasOne(u => u.Silabo)
                .WithMany(s => s.Unidades)
                .HasForeignKey(u => u.SilaboId)
                .OnDelete(DeleteBehavior.Restrict);

            // One-to-Many: Silabo - Carrera
            modelBuilder.Entity<Silabo>()
                .HasOne(s => s.Carrera)
                .WithMany(c => c.Silabos)
                .HasForeignKey(s => s.CarreraId)
                .OnDelete(DeleteBehavior.Restrict);

            // One-to-Many: Silabo - Ciclo
            modelBuilder.Entity<Silabo>()
                .HasOne(s => s.Ciclo)
                .WithMany(c => c.Silabos)
                .HasForeignKey(s => s.CicloId)
                .OnDelete(DeleteBehavior.Restrict);

            // One-to-Many: Parcial - Ciclo
            modelBuilder.Entity<Parcial>()
                .HasOne(p => p.Ciclo)
                .WithMany(c => c.Parciales)
                .HasForeignKey(p => p.CicloId)
                .OnDelete(DeleteBehavior.Restrict);

            // One-to-Many: Ciclo - Periodo
            modelBuilder.Entity<Ciclo>()
                .HasOne(c => c.Periodo)
                .WithMany(p => p.Ciclos)
                .HasForeignKey(c => c.PeriodoId)
                .OnDelete(DeleteBehavior.Restrict);


        }
    }
}
