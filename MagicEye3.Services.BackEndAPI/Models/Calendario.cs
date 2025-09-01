using System.ComponentModel.DataAnnotations;

namespace MagicEye3.Services.BackEndAPI.Models
{
    public class Calendario
    {
        [Key]
        public int CalendarioId { get; set; }
        public string Nombre { get; set; } //lunes a domingo
        public int Numerodia { get; set; }
        public string Mes { get; set; }
        public int Anio { get; set; }
        
        public bool SinClases { get; set; } //true si no hay clases
        public ICollection<AsignaturaCalendario> AsignaturaCalendarios { get; set; } = new List<AsignaturaCalendario>();
    }
}
