using System.ComponentModel.DataAnnotations;

namespace MagicEye3.Services.BackEndAPI.Models
{
    public class Periodo
    {
        [Key]
        public int PeriodoId {  get; set; }
        public string? Nombre {  get; set; }
        public IEnumerable<Silabo>? Silabos { get; set; }
        public IEnumerable<Grupo>? Grupos { get; set; }
    }
}
