using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MagicEye3.Services.BackEndAPI.Models
{
    public class Actividad
    {
        [Key]
        public int ActividadId { get; set; }

        public int EvaluacionId { get; set; }
        [ForeignKey("EvaluacionId")]
        public Evaluacion Evaluacion { get; set; }

        public int Tiempo { get; set; }

        public ICollection<ComponenteActividad> ComponenteActividades { get; set; }
    }
}
