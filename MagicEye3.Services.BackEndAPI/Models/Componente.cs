using System.ComponentModel.DataAnnotations;

namespace MagicEye3.Services.BackEndAPI.Models
{
    public class Componente
    {
        [Key]
        public int ComponenteId { get; set; }
        public string Nombre { get; set; }
        public ICollection<ComponenteActividad> ComponenteActividades { get; set; }
        public ICollection<ContenidoComponente> ContenidoComponentes { get; set; }
    }
}
