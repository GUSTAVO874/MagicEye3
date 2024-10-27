using System.ComponentModel.DataAnnotations;

namespace MagicEye3.Services.BackEndAPI.Models
{
    public class Evaluacion
    {
        [Key]
        public int EvaluacionId { get; set; }
        public string Descripcion {  get; set; }
        
        public IEnumerable<Actividad>? Actividades { get; set; }
    }
}
