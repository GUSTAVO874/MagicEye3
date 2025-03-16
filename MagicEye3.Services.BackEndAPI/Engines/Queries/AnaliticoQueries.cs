using MagicEye3.Services.BackEndAPI.Data;
using MagicEye3.Services.BackEndAPI.Models.Dto;
using MagicEye3.Services.BackEndAPI.Models.Dto.MagicEye3.Services.BackEndAPI.Models.Dto;
using Microsoft.EntityFrameworkCore;

namespace MagicEye3.Services.BackEndAPI.Engines.Queries
{
    public class AnaliticoQueries
    {
        private readonly AppDbContext _context;

        public AnaliticoQueries(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// 1) Consulta principal con JOINs en LINQ.
        ///    Retorna la lista “plana” de ReportAnaliticoDto2
        /// </summary>
        public async Task<List<ReportAnaliticoDto2>> GetDatosAnaliticosAsync()
        {
            var query =
                from u in _context.Unidades
                join s in _context.Silabos on u.SilaboId equals s.SilaboId
                join ci in _context.Ciclos on s.CicloId equals ci.CicloId
                join p in _context.Periodos on ci.PeriodoId equals p.PeriodoId
                join cp in _context.CarreraPeriodos on p.PeriodoId equals cp.PeriodoId
                join car in _context.Carreras on cp.CarreraId equals car.CarreraId

                // LEFT JOIN Parciales
                join paTemp in _context.Parciales on ci.CicloId equals paTemp.CicloId into paLeft
                from pa in paLeft.DefaultIfEmpty()

                    // Contenidos
                join c in _context.Contenidos on u.UnidadId equals c.UnidadId
                // ContenidoActividades
                join ca in _context.ContenidoActividades on c.ContenidoId equals ca.ContenidoId
                // Actividades
                join a in _context.Actividades on ca.ActividadId equals a.ActividadId
                // ComponenteActividades
                join compAct in _context.ComponenteActividades on a.ActividadId equals compAct.ActividadId
                // Componentes
                join co in _context.Componentes on compAct.ComponenteId equals co.ComponenteId
                // Evaluaciones
                join e in _context.Evaluaciones on a.EvaluacionId equals e.EvaluacionId

                // LEFT JOIN FechaContenidos -> Fechas
                join fcTemp in _context.FechaContenidos on c.ContenidoId equals fcTemp.ContenidoId into fcLeft
                from fc in fcLeft.DefaultIfEmpty()
                join fTemp in _context.Fechas on fc.FechaId equals fTemp.FechaId into fLeft
                from f in fLeft.DefaultIfEmpty()

                orderby car.CarreraId, p.PeriodoId, ci.CicloId,
                        pa.ParcialId, u.UnidadId, c.ContenidoId,
                        co.ComponenteId, a.ActividadId

                select new ReportAnaliticoDto2
                {
                    CarreraId = car.CarreraId,
                    CarreraNombre = car.Nombre,

                    PeriodoId = p.PeriodoId,
                    PeriodoNombre = p.Nombre,

                    CicloId = ci.CicloId,
                    CicloIdentificacion = ci.Identificacion,

                    ParcialId = pa != null ? pa.ParcialId : (int?)null,
                    ParcialNombre = pa != null ? pa.Nombre : null,

                    UnidadId = u.UnidadId,
                    UnidadNombre = u.Nombre,
                    UnidadDescripcion = u.Descripcion,

                    ContenidoId = c.ContenidoId,
                    ContenidoDescripcion = c.Descripcion,

                    ComponenteId = co.ComponenteId,
                    ComponenteNombre = co.Nombre,

                    ActividadId = a.ActividadId,
                    TiempoActividad = a.Tiempo,
                    ActividadDescripcion = a.Descripcion,

                    EvaluacionId = e.EvaluacionId,
                    EvaluacionDescripcion = e.Descripcion,

                    FechaId = f != null ? f.FechaId : (int?)null,
                    laFecha = f != null ? f.laFecha : (DateTime?)null,
                };

            var datos = await query.ToListAsync();
            return datos;
        }

        /// <summary>
        /// 2) Agrupación “genérica”: se agrupa por laFecha
        ///    pero NO se suma nada especial para ACD y APE.
        /// </summary>
        public async Task<List<AnaliticoAgrupadoDto>> GetDatosAnaliticosAgrupadosGenericoAsync()
        {
            var datos = await GetDatosAnaliticosAsync();

            // Agrupar por laFecha, sin sumar nada especial => 0
            var agrupado = datos
                .GroupBy(d => d.laFecha)
                .Select(g => new AnaliticoAgrupadoDto
                {
                    laFecha = g.Key,
                    sumaTiemposACDyAPE = 0, // no se aplica
                    Actividades = g.ToList()
                })
                .ToList();

            return agrupado;
        }

        /// <summary>
        /// 3) Agrupación específica para la U. de Guayaquil:
        ///    se agrupa por laFecha y se suma el tiempo de ACD + APE
        /// </summary>
        public async Task<List<AnaliticoAgrupadoDto>> GetDatosAnaliticosAgrupadosGuayaquilAsync()
        {
            var datos = await GetDatosAnaliticosAsync();

            var agrupado = datos
                .GroupBy(d => d.laFecha)
                .Select(g => new AnaliticoAgrupadoDto
                {
                    laFecha = g.Key,
                    sumaTiemposACDyAPE = g
                        .Where(x => x.ComponenteNombre == "ACD" || x.ComponenteNombre == "APE")
                        .Sum(x => x.TiempoActividad),
                    Actividades = g.ToList()
                })
                .ToList();

            return agrupado;
        }
    }

}
