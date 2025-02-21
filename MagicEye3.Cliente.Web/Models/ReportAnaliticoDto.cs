namespace MagicEye3.Cliente.Web.Models
{
    public class ReportAnaliticoDto
    {
        public string Carrera { get; set; }
        public string Periodo { get; set; }
        public string Ciclo { get; set; }
        public string Parcial { get; set; }
        public string Paralelo { get; set; }
        public string NombreUnidad { get; set; }
        public string DescripcionUnidad { get; set; }
        public string NombreSilabo { get; set; }
        public string DescripCont { get; set; }
        public DateOnly FechaImparteCont { get; set; }
        public string CompAprendiz { get; set; }
        public string Actividad { get; set; }
        public string TiempoActividad { get; set; }
        public string Evaluacion { get; set; }
        public string Formato { get; set; }
    }
}
