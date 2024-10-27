namespace MagicEye3.Services.BackEndAPI.Models
{
    public class FechaContenido
    {
        public int ContenidoId { get; set; }
        public Contenido Contenido { get; set; }
        public int FechaId { get; set; }
        public Fecha Fecha {  get; set; }
    }
}
