using jsreport.Client;
using jsreport.Types;

namespace MagicEye3.Services.BackEndAPI.Engines.Render
{
    public class JsReportRenderService
    {
        private readonly IReportingService _reportingService;

        public JsReportRenderService(IReportingService reportingService)
        {
            _reportingService = reportingService;
        }

        /// <summary>
        /// Renderiza el reporte en el formato indicado (pdf, docx, xlsx, etc.),
        /// usando la plantilla apropiada y los helpers indicados.
        /// Devuelve un MemoryStream con el resultado.
        /// 
        /// dataWrapper: debe ser un objeto con la data que la plantilla espera,
        /// por ejemplo: new { Items = (listado agrupado) }.
        /// </summary>
        public async Task<MemoryStream> RenderAnaliticoAsync(object dataWrapper, string format = "pdf")
        {
            // 1) Determinamos templateName (por convención):
            string templateName;
            switch (format.ToLower())
            {
                case "docx":
                    templateName = "/TemplateWord2";
                    break;
                case "xlsx":
                    templateName = "/ExcelTemplate";
                    break;
                default:
                    templateName = "/PdfTemplate";
                    break;
            }

            // 2) Construimos el renderRequest
            var renderRequest = new RenderRequest
            {
                Template = new Template
                {
                    Name = templateName,
                    Engine = Engine.Handlebars,

                    Helpers = @"
                        // Helper 1: uppercase
                        function uppercase(text) {
                            if (!text) return '';
                            return text.toUpperCase();
                        }

                        // Helper 2: formatDate en español (simple)
                        function formatDate(dateString) {
                            if (!dateString) return '';
                            var date = new Date(dateString);
                            return date.toLocaleDateString('es-EC');
                        }

                        // Helper 3: getValueAt
                        function getValueAt(list, index, field) {
                            if (!list || !Array.isArray(list)) {
                                return '1';
                            }
                            if (index < 0 || index >= list.length) {
                                return '2';
                            }
                            var item = list[index];
                            return item[field] || '3';
                        }
                    "
                },
                Data = dataWrapper
            };

            // 3) Renderizamos con jsreport
            var response = await _reportingService.RenderAsync(renderRequest);

            // 4) Pasamos el contenido a un MemoryStream
            var stream = new MemoryStream();
            await response.Content.CopyToAsync(stream);
            stream.Position = 0;

            return stream;
        }
    }
}
