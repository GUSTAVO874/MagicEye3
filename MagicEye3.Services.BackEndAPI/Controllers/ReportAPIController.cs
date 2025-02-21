//funcionaba para generar pdf
//using jsreport.AspNetCore;
//using jsreport.Types;
//using MagicEye3.Services.BackEndAPI.Models.Dto;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;

//namespace MagicEye3.Services.BackEndAPI.Controllers
//{
//    [Route("api/report")]
//    [ApiController]
//    [Authorize(Roles = "ADMIN")]
//    public class ReportController : ControllerBase
//    {
//        private readonly IJsReportMVCService _jsReportService;

//        public ReportController(IJsReportMVCService jsReportService)
//        {
//            _jsReportService = jsReportService;
//        }

//        [HttpPost("generate")]
//        public async Task<IActionResult> GenerateReport([FromBody] ReportDataDto data)
//        {
//            // Cargar la plantilla del reporte
//            var report = await _jsReportService.RenderAsync(new RenderRequest
//            {
//                Template = new Template
//                {
//                    Content = System.IO.File.ReadAllText("Reports/ReportTemplate.html"),
//                    //Content = System.IO.File.ReadAllText("Reports/WordTemplate.docx"),
//                    Engine = Engine.Handlebars,
//                    Recipe = Recipe.ChromePdf
//                    //Recipe = Recipe.Docx,
//                },
//                Data = data
//            });

//            // Devolver el reporte como PDF
//            return File(report.Content, "application/pdf", "Reporte.pdf");
//            //return File(report.Content, "application/docx", "Reporte.docx");
//        }
//    }
//}

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

namespace MagicEye3.Services.BackEndAPI.Controllers
{
    [Route("api/report")]
    [ApiController]
    //[Authorize(Roles = "ADMIN")]
    public class ReportAPIController : ControllerBase
    {
        private readonly ReportingService _reportingService;
        private readonly AppDbContext _context;
        public ReportAPIController(AppDbContext context)
        {
            _context = context;
            // Configuración del cliente jsreport
            _reportingService = new ReportingService("http://localhost:5488", "admin", "password");
            //_reportingService = new ReportingService("http://127.0.0.1:5488", "admin", "password");
        }

        //[HttpPost("generate")]
        //public async Task<IActionResult> GenerateReport([FromBody] ReportDataDto data, [FromQuery] string format = "pdf")
        //{
        //    try { 
        //    string templateName;
        //    string contentType;
        //    string fileName;

        //    if (format.ToLower() == "docx")
        //    {
        //        templateName = "/WordTemplate";
        //        contentType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
        //        fileName = "Reporte.docx";
        //    }
        //    else if (format.ToLower() == "xlsx")
        //    {
        //        templateName = "/ExcelTemplate";
        //        contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        //        fileName = "Reporte.xlsx";
        //    }
        //    else // Default to PDF
        //    {
        //        templateName = "/PdfTemplate";
        //        contentType = "application/pdf";
        //        fileName = "Reporte.pdf";
        //    }

        //    // Configuración de la solicitud de renderizado
        //    var renderRequest = new RenderRequest
        //    {
        //        Template = new Template
        //        {
        //            Name = templateName,
        //            Engine = Engine.Handlebars,
        //        },
        //        Data = data
        //    };

        //    // Generar el reporte
        //    var response = await _reportingService.RenderAsync(renderRequest);

        //    // Retornar el archivo generado
        //    using (var stream = new MemoryStream())
        //    {
        //        await response.Content.CopyToAsync(stream);
        //        stream.Position = 0;
        //        return File(stream, contentType, fileName);

        //    }
        //    }
        //    catch (Exception ex)
        //    {
        //        // Registrar el error
        //        Console.Error.WriteLine($"Error al generar el reporte: {ex.Message}");
        //        return StatusCode(500, "Error interno al generar el reporte.");
        //    }
        //}
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
        //[HttpPost("generateanalitic")]
        //public async Task<IActionResult> GenerateReportAnalitic([FromBody] ReportAnaliticoDto data, [FromQuery] string format = "pdf")
        //{
        //    try
        //    {
        //        string templateName;
        //        string contentType;
        //        string fileName;

        //        if (format.ToLower() == "docx")
        //        {
        //            templateName = "/WordTemplate";
        //            contentType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
        //            fileName = "Reporte.docx";
        //        }
        //        else if (format.ToLower() == "xlsx")
        //        {
        //            templateName = "/ExcelTemplate";
        //            contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        //            fileName = "Reporte.xlsx";
        //        }
        //        else // Default to PDF
        //        {
        //            templateName = "/PdfTemplate";
        //            contentType = "application/pdf";
        //            fileName = "Reporte.pdf";
        //        }

        //        // Configuración de la solicitud de renderizado
        //        var renderRequest = new RenderRequest
        //        {
        //            Template = new Template
        //            {
        //                Name = templateName,
        //                Engine = Engine.Handlebars,
        //            },
        //            Data = data
        //        };

        //        // Generar el reporte
        //        var response = await _reportingService.RenderAsync(renderRequest);

        //        // Retornar el archivo generado sin el bloque using
        //        var stream = new MemoryStream();
        //        await response.Content.CopyToAsync(stream);
        //        stream.Position = 0;
        //        return File(stream, contentType, fileName);
        //    }
        //    catch (Exception ex)
        //    {
        //        // Registrar el error y retornar una respuesta de error
        //        Console.Error.WriteLine($"Error al generar el reporte: {ex.Message}");
        //        return StatusCode(500, "Error interno al generar el reporte.");
        //    }
        //}
    }
}


