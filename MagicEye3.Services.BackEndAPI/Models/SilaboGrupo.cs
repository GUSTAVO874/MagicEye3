namespace MagicEye3.Services.BackEndAPI.Models
{
    public class SilaboGrupo
    {
        public int SilaboId { get; set; }
        public Silabo Silabo { get; set; }
        public int GrupoId { get; set; }
        public Grupo Grupo { get; set; }
        public string Dia { get; set; }
        public string HoraInicio { get; set; }
        public string HoraFin { get; set; }
    }
}
