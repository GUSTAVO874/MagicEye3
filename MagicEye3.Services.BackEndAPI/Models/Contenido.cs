using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MagicEye3.Services.BackEndAPI.Models
{
    public class Contenido
    {
        [Key]
        public int ContenidoId { get; set; }
        public int UnidadId { get; set; }
        
        [ForeignKey("UnidadId")]
        public Unidad? Unidad { get; set; }

        public string? Descripcion { get; set; } //por ejemplo tipos primitivos, descrip de datos, etc.
        
        public ICollection<ContenidoActividad> ContenidoActividades { get; set; }
        public ICollection<FechaContenido> FechaContenidos { get; set; }

    }
}
