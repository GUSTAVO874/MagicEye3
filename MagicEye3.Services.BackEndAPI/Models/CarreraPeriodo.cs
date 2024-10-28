namespace MagicEye3.Services.BackEndAPI.Models
{
    public class CarreraPeriodo
    {
        public int CarreraId { get; set; }
        public Carrera Carrera { get; set; }
        public int PeriodoId { get; set; }
        public Periodo Periodo { get; set; }
    }
}
