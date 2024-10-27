using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MagicEye3.Services.BackEndAPI.Models
{
    public class Parcial
    {
        [Key]
        public int ParcialId { get; set; }
        public string? Nombre { get; set; }

        // Add this navigation property
        public ICollection<SilaboParcial> SilaboParciales { get; set; }
    }
}
