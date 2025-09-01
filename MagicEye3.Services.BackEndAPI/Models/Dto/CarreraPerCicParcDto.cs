using System.ComponentModel.DataAnnotations;

namespace MagicEye3.Services.BackEndAPI.Models.Dto
{
    public class CarreraPerCicParcDto
    {
        [Required(ErrorMessage = "La carrera es obligatoria.")]
        public CarreraDto Carrera { get; set; }
        public PeriodoDto Periodo { get; set; }

        // Agrega estos dos si vas a guardar Ciclo y Parcial
        public CicloDto Ciclo { get; set; }
        public ParcialDto Parcial { get; set; }
    }
}
