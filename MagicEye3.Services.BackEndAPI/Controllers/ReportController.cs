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
using MagicEye3.Services.BackEndAPI.Models.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Threading.Tasks;

namespace MagicEye3.Services.BackEndAPI.Controllers
{
    [Route("api/report")]
    [ApiController]
    [Authorize(Roles = "ADMIN")]
    public class ReportController : ControllerBase
    {
        private readonly ReportingService _reportingService;

        public ReportController()
        {
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
                var renderRequest = new RenderRequest
                {
                    Template = new Template
                    {
                        Name = templateName,
                        Engine = Engine.Handlebars,
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

    }
}


