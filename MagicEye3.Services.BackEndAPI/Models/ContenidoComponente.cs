namespace MagicEye3.Services.BackEndAPI.Models
{
    public class ContenidoComponente
    {
        public int ContenidoId { get; set; }
        public Contenido Contenido { get; set; }
        public int ComponenteId { get; set; }
        public Componente Componente { get; set; }
    }
}
