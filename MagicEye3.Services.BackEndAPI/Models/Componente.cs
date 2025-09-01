using System.ComponentModel.DataAnnotations;

namespace MagicEye3.Services.BackEndAPI.Models
{
    public class Componente
    {
        [Key]
        public int ComponenteId { get; set; }
        public string Nombre { get; set; }
        public ICollection<ComponenteActividad> ComponenteActividades { get; set; } = new List<ComponenteActividad>();
        public ICollection<SilaboComponente> SilaboComponentes { get; set; } = new List<SilaboComponente>();
        //public ICollection<ContenidoComponente> ContenidoComponentes { get; set; }
    }
}
