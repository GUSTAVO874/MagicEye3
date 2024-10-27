using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MagicEye3.Services.BackEndAPI.Models
{
    public class Grupo
    {
        [Key]
        public int GrupoId {  get; set; }
        public int PeriodoId { get; set; }
        [ForeignKey("PeriodoId")]
        public Periodo? Periodo { get; set; }

        public string Nombre { get; set; }

    }
}
