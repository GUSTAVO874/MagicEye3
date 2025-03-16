namespace MagicEye3.Services.BackEndAPI.Models.Dto
{
    public class ReportAnaliticoDto2
    {
        public int CarreraId { get; set; }
        public string CarreraNombre { get; set; }

        public int PeriodoId { get; set; }
        public string PeriodoNombre { get; set; }

        public int CicloId { get; set; }
        public string CicloIdentificacion { get; set; }

        public int? ParcialId { get; set; }
        public string ParcialNombre { get; set; }

        public int UnidadId { get; set; }
        public string UnidadNombre { get; set; }
        public string UnidadDescripcion { get; set; }

        public int ContenidoId { get; set; }
        public string ContenidoDescripcion { get; set; }

        public int ComponenteId { get; set; }
        public string ComponenteNombre { get; set; }

        public int ActividadId { get; set; }
        public int TiempoActividad { get; set; }
        public string ActividadDescripcion { get; set; }

        public int EvaluacionId { get; set; }
        public string EvaluacionDescripcion { get; set; }

        public int? FechaId { get; set; }
        public DateTime? laFecha { get; set; }

        
    }

}
