using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MagicEye3.Services.BackEndAPI.Models
{
    public class Silabo
    {
        [Key]
        public int SilaboId { get; set; }
        public int CarreraId { get; set; }
        [ForeignKey("CarreraId")]
        public Carrera? Carrera { get; set; }

        public int PeriodoId { get; set; }
        [ForeignKey("PeriodoId")]
        public Periodo? Periodo { get; set; }

        public string? Nombre { get; set; } // nombre de la asignatura
        public ICollection<Unidad>? Unidades { get; set; }

        // Add this navigation property
        public ICollection<SilaboParcial> SilaboParciales { get; set; }
    }
}
