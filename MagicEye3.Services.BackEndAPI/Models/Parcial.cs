using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MagicEye3.Services.BackEndAPI.Models
{
    public class Parcial
    {
        [Key]
        public int ParcialId { get; set; }
        public string? Nombre { get; set; }
        public int CicloId { get; set; }
        [ForeignKey("CicloId")]
        public Ciclo? Ciclo { get; set; }

    }
}
