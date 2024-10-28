using System.ComponentModel.DataAnnotations;

namespace MagicEye3.Services.BackEndAPI.Models
{
    public class Periodo
    {
        [Key]
        public int PeriodoId {  get; set; }
        public string? Nombre {  get; set; }
        public IEnumerable<CarreraPeriodo>? CarreraPeriodos { get; set; }
        public IEnumerable<Ciclo>? Ciclos{ get; set; }

    }
}
