namespace MagicEye3.Services.BackEndAPI.Models
{
    public class ContenidoActividad
    {
        public int ContenidoId { get; set; }
        public Contenido Contenido { get; set; }
        public int ActividadId { get; set; }
        public Actividad Actividad { get; set; }
    }
}
