using System.ComponentModel.DataAnnotations;

namespace MagicEye3.Services.BackEndAPI.Models
{
    public class Asignatura
    {
        [Key]
        public int AsignaturaId { get; set; }
        public string Nombre { get; set; }
        //public ICollection<AsignaturaDia> AsignaturaDias { get; set; } = new List<AsignaturaDia>();
        public ICollection<AsignaturaCalendario> AsignaturaCalendarios { get; set; } = new List<AsignaturaCalendario>();

    }

}
