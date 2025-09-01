namespace MagicEye3.Services.BackEndAPI.Models
{
    public class AsignaturaCalendario
    {
        public int CalendarioId { get; set; }
        public int AsignaturaId { get; set; }
        public TimeSpan HoraInicio { get; set; }
        public TimeSpan HoraFin { get; set; }
        public bool Examen { get; set; }
        public bool InicioClases { get; set; }
        public bool FinClases { get; set; }


        public Asignatura Asignatura { get; set; }
        public Calendario Calendario{ get; set; }
    }
}
