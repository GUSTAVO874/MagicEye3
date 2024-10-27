namespace MagicEye3.Services.BackEndAPI.Models
{
    public class ComponenteActividad
    {
        public int ComponenteId { get; set; }
        public Componente Componente { get; set; }
        public int ActividadId { get; set; }
        public Actividad Actividad { get; set; }
    }
}
