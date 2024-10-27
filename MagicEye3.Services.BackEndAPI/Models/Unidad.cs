using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MagicEye3.Services.BackEndAPI.Models
{
    public class Unidad
    {
        [Key]
        public int UnidadId { get; set; }
        public int SilaboId {  get; set; }
        [ForeignKey("SilaboId")]
        public Silabo? Silabo { get; set; }

        public string Nombre { get; set; }
        public string Descripcion { get; set;}

        public IEnumerable<Contenido>? Contenidos { get; set; }
    }
}
