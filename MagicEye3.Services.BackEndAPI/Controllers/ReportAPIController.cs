using jsreport.Client;
using jsreport.Types;
using MagicEye3.Services.BackEndAPI.Data;
using MagicEye3.Services.BackEndAPI.Models.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Threading.Tasks;
using HandlebarsDotNet;
using Microsoft.Extensions.Options;
using System.Drawing;
using MagicEye3.Services.BackEndAPI.Engines.Queries;
using MagicEye3.Services.BackEndAPI.Engines.Render;


namespace MagicEye3.Services.BackEndAPI.Controllers
{
    [Route("api/report")]
    [ApiController]
    //[Authorize(Roles = "ADMIN")]
    public class ReportAPIController : ControllerBase
    {
        private readonly ReportingService _reportingService;
        private readonly AppDbContext _context;

        private readonly AnaliticoQueries _analiticoQueries;
        private readonly JsReportRenderService _renderService;

        public ReportAPIController(AppDbContext context, AnaliticoQueries analiticoQueries,
            JsReportRenderService renderService)
        {
            _context = context;
            // Configuración del cliente jsreport..
            _reportingService = new ReportingService("http://localhost:5488", "admin", "password");
            //_reportingService = new ReportingService("http://127.0.0.1:5488", "admin", "password");

            _analiticoQueries = analiticoQueries;
            _renderService = renderService;
        }


        [HttpPost("generate")]
        public async Task<IActionResult> GenerateReport([FromBody] ReportDataDto data, [FromQuery] string format = "pdf")
        {
            try
            {
                string templateName;
                string contentType;
                string fileName;

                if (format.ToLower() == "docx")
                {
                    templateName = "/TemplateWord2";
                    contentType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
                    fileName = "Reporte.docx";
                }
                else if (format.ToLower() == "xlsx")
                {
                    templateName = "/ExcelTemplate";
                    contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    fileName = "Reporte.xlsx";
                }
                else // Default to PDF
                {
                    templateName = "/PdfTemplate";
                    contentType = "application/pdf";
                    fileName = "Reporte.pdf";
                }

                // Configuración de la solicitud de renderizado
                var renderRequest = new RenderRequest
                {
                    Template = new Template
                    {
                        Name = templateName,
                        Engine = Engine.Handlebars
                    },
                    Data = data
                };

                // Generar el reporte
                var response = await _reportingService.RenderAsync(renderRequest);

                // Retornar el archivo generado sin el bloque using
                var stream = new MemoryStream();
                await response.Content.CopyToAsync(stream);
                stream.Position = 0;
                return File(stream, contentType, fileName);
            }
            catch (Exception ex)
            {
                // Registrar el error y retornar una respuesta de error
                Console.Error.WriteLine($"Error al generar el reporte: {ex.Message}");
                return StatusCode(500, "Error interno al generar el reporte.");
            }
        }

        [HttpGet("Analitico/{carrera}/{periodo}/{ciclo}/{parcial}/{paralelo}")]
        //public async Task<IActionResult> GenerateReport([FromBody] ReportDataDto data, [FromQuery] string format = "pdf")
        //public async Task<ActionResult<IEnumerable<ReportAnaliticoDto>>> GetAnalitico(
        public async Task<IActionResult> GenerateReport2(
            string carrera,
            string periodo,
            string ciclo,
            string parcial,
            string paralelo,
            [FromQuery] string format = "pdf")
        {
            try
            {
                // Construimos la consulta con LINQ (syntax estilo SQL)
                var query =
                    from cp in _context.CarreraPeriodos
                    join c in _context.Carreras on cp.CarreraId equals c.CarreraId
                    join pe in _context.Periodos on cp.PeriodoId equals pe.PeriodoId
                    join ci in _context.Ciclos on pe.PeriodoId equals ci.PeriodoId
                    join pa in _context.Parciales on ci.CicloId equals pa.CicloId
                    join s in _context.Silabos on ci.CicloId equals s.CicloId
                    join sg in _context.SilaboGrupos on s.SilaboId equals sg.SilaboId
                    join g in _context.Grupos on sg.GrupoId equals g.GrupoId
                    join u in _context.Unidades on s.SilaboId equals u.SilaboId
                    join co in _context.Contenidos on u.UnidadId equals co.UnidadId
                    join fc in _context.FechaContenidos on co.ContenidoId equals fc.ContenidoId
                    join f in _context.Fechas on fc.FechaId equals f.FechaId
                    join cact in _context.ContenidoActividades on co.ContenidoId equals cact.ContenidoId
                    join compAct in _context.ComponenteActividades on cact.ActividadId equals compAct.ActividadId
                    join comp in _context.Componentes on compAct.ComponenteId equals comp.ComponenteId
                    join act in _context.Actividades on compAct.ActividadId equals act.ActividadId
                    join ev in _context.Evaluaciones on act.EvaluacionId equals ev.EvaluacionId

                    // Filtramos por los parámetros
                    where c.Nombre == carrera
                       && pe.Nombre == periodo
                       && ci.Identificacion == ciclo
                       && pa.Nombre == parcial
                       && g.Nombre == paralelo

                    // Construimos el DTO
                    select new ReportAnaliticoDto
                    {
                        Carrera = c.Nombre,
                        Periodo = pe.Nombre,
                        Ciclo = ci.Identificacion,
                        Parcial = pa.Nombre,
                        Paralelo = g.Nombre,

                        NombreUnidad = u.Nombre,
                        DescripcionUnidad = u.Descripcion,
                        NombreSilabo = s.Nombre,
                        DescripCont = co.Descripcion,

                        // Asumiendo f.LaFecha es DateTime en tu clase Fecha
                        FechaImparteCont = DateOnly.FromDateTime(f.laFecha),

                        CompAprendiz = comp.Nombre,
                        Actividad = act.Descripcion,
                        TiempoActividad = act.Tiempo.ToString(),
                        Evaluacion = ev.Descripcion,

                        // Suponiendo que la columna "Formato" esté en Evaluacion o Actividad 
                        // (ajusta según tu modelo):
                        //Formato = ev.Formato
                        // o "Formato = act.Formato" si está en la tabla Actividad, etc.
                    };

                var datos = await query.ToListAsync();

                string templateName;
                string contentType;
                string fileName;

                if (format.ToLower() == "docx")
                {
                    templateName = "/WordTemplate";
                    contentType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
                    fileName = "Reporte.docx";
                }
                else if (format.ToLower() == "xlsx")
                {
                    templateName = "/ExcelTemplate";
                    contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    fileName = "Reporte.xlsx";
                }
                else // Default to PDF
                {
                    templateName = "/PdfTemplate";
                    contentType = "application/pdf";
                    fileName = "Reporte.pdf";
                }

                // Configuración de la solicitud de renderizado
                //var renderRequest = new RenderRequest
                //{
                //    Template = new Template
                //    {
                //        Name = templateName,
                //        Engine = Engine.Handlebars,
                //    },
                //    Data = datos
                //};

                //////////////
                ///
                // En lugar de Data = datos, envolverlo en un objeto
                var dataWrapper = new
                {
                    Items = datos   // El nombre "Items" puede ser el que tú quieras
                };

                //
                Console.WriteLine(System.Text.Json.JsonSerializer.Serialize(dataWrapper,
                    new System.Text.Json.JsonSerializerOptions { WriteIndented = true }));
                System.Diagnostics.Debug.WriteLine(System.Text.Json.JsonSerializer.Serialize(dataWrapper,
                    new System.Text.Json.JsonSerializerOptions { WriteIndented = true }));


                var renderRequest = new RenderRequest
                {
                    Template = new Template
                    {
                        Name = templateName,
                        Engine = Engine.Handlebars,  // o el engine que uses
                    },
                    Data = dataWrapper
                };
                //////////

                // Generar el reporte
                var response = await _reportingService.RenderAsync(renderRequest);

                // Retornar el archivo generado sin el bloque using
                var stream = new MemoryStream();
                await response.Content.CopyToAsync(stream);
                stream.Position = 0;
                return File(stream, contentType, fileName);

                //return Ok(datos);
            }
            catch (Exception ex)
            {
                // Manejo de errores
                return StatusCode(500, $"Error al obtener datos: {ex.Message}");
            }
        }


        //    [HttpGet("Analitico2")]
        //    public async Task<IActionResult> GenerateReport3(
        ////string carrera,
        ////string periodo,
        ////string ciclo,
        ////string parcial,
        ////string paralelo,
        //[FromQuery] string format = "pdf")
        //    {
        //        try
        //        {
        //            // ***** 1) Construimos la query con LINQ *****
        //            // Igual que tu SQL, pero en LINQ:
        //            var query =
        //                from u in _context.Unidades
        //                join s in _context.Silabos on u.SilaboId equals s.SilaboId
        //                join ci in _context.Ciclos on s.CicloId equals ci.CicloId
        //                join p in _context.Periodos on ci.PeriodoId equals p.PeriodoId
        //                join cp in _context.CarreraPeriodos on p.PeriodoId equals cp.PeriodoId
        //                join car in _context.Carreras on cp.CarreraId equals car.CarreraId

        //                // LEFT JOIN Parciales -> 'pa'
        //                join paTemp in _context.Parciales on ci.CicloId equals paTemp.CicloId into paLeft
        //                from pa in paLeft.DefaultIfEmpty()

        //                    // Contenidos
        //                join c in _context.Contenidos on u.UnidadId equals c.UnidadId
        //                // ContenidoActividades
        //                join ca in _context.ContenidoActividades on c.ContenidoId equals ca.ContenidoId
        //                // Actividades
        //                join a in _context.Actividades on ca.ActividadId equals a.ActividadId
        //                // ComponenteActividades
        //                join compAct in _context.ComponenteActividades on a.ActividadId equals compAct.ActividadId
        //                // Componentes
        //                join co in _context.Componentes on compAct.ComponenteId equals co.ComponenteId
        //                // Evaluaciones
        //                join e in _context.Evaluaciones on a.EvaluacionId equals e.EvaluacionId

        //                // LEFT JOIN FechaContenidos/Fechas
        //                join fcTemp in _context.FechaContenidos on c.ContenidoId equals fcTemp.ContenidoId into fcLeft
        //                from fc in fcLeft.DefaultIfEmpty()
        //                join fTemp in _context.Fechas on fc.FechaId equals fTemp.FechaId into fLeft
        //                from f in fLeft.DefaultIfEmpty()

        //                orderby car.CarreraId, p.PeriodoId, ci.CicloId,
        //                        pa.ParcialId, u.UnidadId, c.ContenidoId,
        //                        co.ComponenteId, a.ActividadId

        //                // ***** 2) Proyectamos a tu nuevo DTO *****
        //                select new ReportAnaliticoDto2
        //                {
        //                    CarreraId = car.CarreraId,
        //                    CarreraNombre = car.Nombre,

        //                    PeriodoId = p.PeriodoId,
        //                    PeriodoNombre = p.Nombre,

        //                    CicloId = ci.CicloId,
        //                    CicloIdentificacion = ci.Identificacion,

        //                    ParcialId = pa != null ? pa.ParcialId : (int?)null,
        //                    ParcialNombre = pa != null ? pa.Nombre : null,

        //                    UnidadId = u.UnidadId,
        //                    UnidadNombre = u.Nombre,
        //                    UnidadDescripcion = u.Descripcion,

        //                    ContenidoId = c.ContenidoId,
        //                    ContenidoDescripcion = c.Descripcion,

        //                    ComponenteId = co.ComponenteId,
        //                    ComponenteNombre = co.Nombre,

        //                    ActividadId = a.ActividadId,
        //                    TiempoActividad = a.Tiempo,  // int
        //                    ActividadDescripcion = a.Descripcion,

        //                    EvaluacionId = e.EvaluacionId,
        //                    EvaluacionDescripcion = e.Descripcion,

        //                    FechaId = f != null ? f.FechaId : (int?)null,
        //                    laFecha = f != null ? f.laFecha : (DateTime?)null
        //                };

        //            // *** 3) Ejecutamos la query ***
        //            //var datos = await query.ToListAsync();

        //            // *** 4) El resto de tu lógica EXACTAMENTE igual ***
        //            string templateName;
        //            string contentType;
        //            string fileName;

        //            if (format.ToLower() == "docx")
        //            {
        //                templateName = "/TemplateWord2";
        //                contentType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
        //                fileName = "Reporte.docx";
        //            }
        //            else if (format.ToLower() == "xlsx")
        //            {
        //                templateName = "/ExcelTemplate";
        //                contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        //                fileName = "Reporte.xlsx";
        //            }
        //            else // Default to PDF
        //            {
        //                templateName = "/PdfTemplate";
        //                contentType = "application/pdf";
        //                fileName = "Reporte.pdf";
        //            }


        //            // 1) Ejecutas tu misma consulta en LINQ, pero obtienes la lista "datos" como antes.
        //            var datos = await query.ToListAsync();

        //            // 2) Agrupas por la fecha
        //            var agrupadoPorFecha = datos
        //                .GroupBy(d => d.laFecha)  // agrupa por el campo DateTime? laFecha
        //                .Select(g => new
        //                {
        //                    laFecha = g.Key,
        //                    // Aquí puedes renombrar a "Items" o como gustes
        //                    Actividades = g.ToList()
        //                })
        //                .ToList();

        //            // 3) Creas el wrapper que enviarás a la plantilla
        //            var dataWrapper = new
        //            {
        //                // Si quieres, el nombre "Items" puede seguir siendo este,
        //                // pero adentro ya vienen sub-listas para cada fecha:
        //                Items = agrupadoPorFecha
        //            };



        //            {

        //                //var dataWrapper = new
        //                //{
        //                //    Items = datos
        //                //};

        //                //
        //                Console.WriteLine(System.Text.Json.JsonSerializer.Serialize(dataWrapper,
        //                    new System.Text.Json.JsonSerializerOptions { WriteIndented = true }));
        //                System.Diagnostics.Debug.WriteLine(System.Text.Json.JsonSerializer.Serialize(dataWrapper,
        //                    new System.Text.Json.JsonSerializerOptions { WriteIndented = true }));

        //                var renderRequest = new RenderRequest
        //                {
        //                    Template = new Template
        //                    {
        //                        Name = templateName,
        //                        Engine = Engine.Handlebars
        //                    },
        //                    Data = dataWrapper
        //                };

        //                var response = await _reportingService.RenderAsync(renderRequest);

        //                var stream = new MemoryStream();
        //                await response.Content.CopyToAsync(stream);
        //                stream.Position = 0;
        //                return File(stream, contentType, fileName);
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            return StatusCode(500, $"Error al obtener datos: {ex.Message}");
        //        }
        //    }

        [HttpGet("Analitico2")]
        public async Task<IActionResult> GenerateReport3(
        [FromQuery] string format = "pdf")
        {
            try
            {
                // ***** 1) Construimos la query con LINQ *****
                // Igual que tu SQL, pero en LINQ:
                var query =
                    from u in _context.Unidades
                    join s in _context.Silabos on u.SilaboId equals s.SilaboId
                    join ci in _context.Ciclos on s.CicloId equals ci.CicloId
                    join p in _context.Periodos on ci.PeriodoId equals p.PeriodoId
                    join cp in _context.CarreraPeriodos on p.PeriodoId equals cp.PeriodoId
                    join car in _context.Carreras on cp.CarreraId equals car.CarreraId

                    // LEFT JOIN Parciales -> 'pa'
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

                    // LEFT JOIN FechaContenidos/Fechas
                    join fcTemp in _context.FechaContenidos on c.ContenidoId equals fcTemp.ContenidoId into fcLeft
                    from fc in fcLeft.DefaultIfEmpty()
                    join fTemp in _context.Fechas on fc.FechaId equals fTemp.FechaId into fLeft
                    from f in fLeft.DefaultIfEmpty()

                    orderby car.CarreraId, p.PeriodoId, ci.CicloId,
                            pa.ParcialId, u.UnidadId, c.ContenidoId,
                            co.ComponenteId, a.ActividadId

                    // ***** 2) Proyectamos a tu nuevo DTO *****
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
                        TiempoActividad = a.Tiempo,  // int
                        ActividadDescripcion = a.Descripcion,

                        EvaluacionId = e.EvaluacionId,
                        EvaluacionDescripcion = e.Descripcion,

                        FechaId = f != null ? f.FechaId : (int?)null,
                        laFecha = f != null ? f.laFecha : (DateTime?)null,

                        
                    };

                // *** 3) Ejecutamos la query ***
                var datos = await query.ToListAsync();

                //CÓDIGO FUNCIONANDO PARA OTRAS UNIVERSIDADES
                // *** 4) Agrupamos por fecha (ejemplo), si así lo deseas ***
                //var agrupadoPorFecha = datos
                //    .GroupBy(d => d.laFecha) // agrupa por el campo DateTime? laFecha
                //    .Select(g => new
                //    {
                //        laFecha = g.Key,
                //        Actividades = g.ToList()
                //    })
                //    .ToList();

                //CÓDKGO FUNCIONANDO PARA UNIVERSIDAD DE GUAYAQUIL
                //agrupado por fecha y sumado los tiempos según normativa actual UG
                var agrupadoPorFecha = datos
                    .GroupBy(d => d.laFecha) // agrupa por laFecha
                    .Select(g => new
                    {
                        laFecha = g.Key,
                        sumaTiemposACDyAPE = g
                        .Where(x => x.ComponenteNombre == "ACD" || x.ComponenteNombre == "APE")
                        .Sum(x => x.TiempoActividad),
                        Actividades = g.ToList()
                    })
                    .ToList();


                //solo para visualizar en consola, se puede comentar
                Console.WriteLine(System.Text.Json.JsonSerializer.Serialize(agrupadoPorFecha,
                                    new System.Text.Json.JsonSerializerOptions { WriteIndented = true }));
                                System.Diagnostics.Debug.WriteLine(System.Text.Json.JsonSerializer.Serialize(agrupadoPorFecha,
                                    new System.Text.Json.JsonSerializerOptions { WriteIndented = true }));


                // *** 5) Creamos el data wrapper que se enviará a la plantilla ***
                var dataWrapper = new
                {
                    Items = agrupadoPorFecha
                };

                // *** 6) Determinamos formato y nombre de plantilla
                string templateName;
                string contentType;
                string fileName;

                if (format.ToLower() == "docx")
                {
                    templateName = "/TemplateWord2";
                    contentType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
                    fileName = "Reporte.docx";
                }
                else if (format.ToLower() == "xlsx")
                {
                    templateName = "/ExcelTemplate";
                    contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    fileName = "Reporte.xlsx";
                }
                else // Default to PDF
                {
                    templateName = "/PdfTemplate";
                    contentType = "application/pdf";
                    fileName = "Reporte.pdf";
                }

                // *** 7) Preparamos la petición de renderizado a jsreport ***
                var renderRequest = new RenderRequest
                {
                    Template = new Template
                    {
                        Name = templateName,
                        Engine = Engine.Handlebars,

                        // === OJO AQUÍ: REGISTRAMOS LOS HELPERS JS ===
                        Helpers = @"
                    // Helper 1: uppercase: convierte un texto a mayúsculas
                    function uppercase(text) {
                        if (!text) return '';
                        return text.toUpperCase();
                    }

                    // Helper 2 (ejemplo): formatDate -> formatea una fecha en español (simple)
                    function formatDate(dateString) {
                        if (!dateString) return '';
                        var date = new Date(dateString);
                        // Ejemplo: '13/03/2025'
                        return date.toLocaleDateString('es-EC');
                    }
                     
                    /**
                     * getValueAt(list, index, field)
                     * Devuelve la propiedad 'field' del objeto en la posición 'index' dentro de 'list'
                     */
                    function getValueAt(list, index, field) {
                        if (!list || !Array.isArray(list)) {
                            return '1';
                        }
                        if (index < 0 || index >= list.length) {
                            return '2';
                        }
                        // Obtenemos el objeto
                        var item = list[index];
                        // Devolvemos la propiedad
                        return item[field] || '3';
                    }

                "
                    },
                    Data = dataWrapper
                };

                // *** 8) Enviamos la solicitud y obtenemos el reporte ***
                var response = await _reportingService.RenderAsync(renderRequest);

                // *** 9) Retornamos el archivo en la respuesta ***
                var stream = new MemoryStream();
                await response.Content.CopyToAsync(stream);
                stream.Position = 0;
                return File(stream, contentType, fileName);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al obtener datos: {ex.Message}");
            }
        }
        [HttpGet("Analitico3")]
        public async Task<IActionResult> GenerateReport4(
            [FromQuery] string format = "pdf",
            [FromQuery] string universidad = "generico")
        {
            try
            {
                // 1) Obtenemos el listado agrupado según la universidad
                var datosAgrupados =
                    universidad.ToLower() == "guayaquil"
                    ? await _analiticoQueries.GetDatosAnaliticosAgrupadosGuayaquilAsync()
                    : await _analiticoQueries.GetDatosAnaliticosAgrupadosGenericoAsync();

                // 2) Empaquetamos en dataWrapper
                var dataWrapper = new
                {
                    Items = datosAgrupados
                };

                // 3) Renderizamos con jsreport
                var stream = await _renderService.RenderAnaliticoAsync(dataWrapper, format);

                // 4) Determinamos contentType y fileName
                string contentType;
                string fileName;

                switch (format.ToLower())
                {
                    case "docx":
                        contentType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
                        fileName = "Reporte.docx";
                        break;
                    case "xlsx":
                        contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                        fileName = "Reporte.xlsx";
                        break;
                    default:
                        contentType = "application/pdf";
                        fileName = "Reporte.pdf";
                        break;
                }

                // 5) Retornamos el archivo
                return File(stream, contentType, fileName);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al obtener datos: {ex.Message}");
            }
        }
    }
}


