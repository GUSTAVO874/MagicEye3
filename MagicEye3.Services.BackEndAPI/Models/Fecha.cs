using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MagicEye3.Services.BackEndAPI.Models
{
    public class Fecha
    {
        [Key]
        public int FechaId { get; set; }
        public DateOnly laFecha { get; set; }

        // Add this navigation property
        public ICollection<FechaContenido> FechaContenidos { get; set; }
    }
}
