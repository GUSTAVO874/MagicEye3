using System.ComponentModel.DataAnnotations;

namespace MagicEye3.Services.BackEndAPI.Models.Dto
{
    public class CarreraDto
    {
        public int CarreraId { get; set; }
        
        [StringLength(3, ErrorMessage = "El nombre de la carrera no debe exceder 3 caracteres.")]
        public string? Nombre { get; set; }
    }
}
