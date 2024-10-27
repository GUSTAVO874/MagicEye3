using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MagicEye3.Services.BackEndAPI.Models
{
    public class Ciclo
    {
        [Key]
        public int CicloId { get; set; }
        public string Identificacion { get; set; }

        public int PeriodoId { get; set; }
        [ForeignKey("PeriodoId")]
        public Periodo Periodo { get; set; }

        public ICollection<Silabo> Silabos { get; set; }
        public ICollection<Parcial> Parciales { get; set; }
    }

}
