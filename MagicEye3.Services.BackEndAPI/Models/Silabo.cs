using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MagicEye3.Services.BackEndAPI.Models
{
    public class Silabo
    {
        [Key]
        public int SilaboId { get; set; }
        public int CicloId { get; set; }
        [ForeignKey("CicloId")]
        public Ciclo? Ciclo { get; set; }
        
        public int AsignaturaId { get; set; } // foreign key to Asignatura
        [ForeignKey("AsignaturaId")]
        public Asignatura? Asignatura { get; set; }

        public string? Version { get; set; } // nombre de la asignatura
        public ICollection<Unidad> Unidades { get; set; } = new List<Unidad>();

        // Add this navigation property
        public ICollection<SilaboGrupo> SilaboGrupos { get; set; } = new List<SilaboGrupo>();
        public ICollection<SilaboComponente> SilaboComponentes { get; set; } = new List<SilaboComponente>();

    }
}
