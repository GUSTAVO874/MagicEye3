using System.ComponentModel.DataAnnotations;

namespace MagicEye3.Services.BackEndAPI.Models
{
    public class Carrera
    {
        [Key]
        public int CarreraId { get; set; }
        public string? Nombre { get; set; }
        public IEnumerable<Silabo>? Silabos { get; set; }
    }
}
