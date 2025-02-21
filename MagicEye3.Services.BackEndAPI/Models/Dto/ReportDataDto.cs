namespace MagicEye3.Services.BackEndAPI.Models.Dto
{
    public class ReportDataDto
    {
        public string Nombre { get; set; }
        public int Edad { get; set; }
        public string Correo { get; set; }

        
        // Lista con valores predeterminados para probar la plantilla de jsreport
        public List<string> Roles { get; set; } = new List<string> { "TestAdmin", "User", "Guest" };
    }
}
