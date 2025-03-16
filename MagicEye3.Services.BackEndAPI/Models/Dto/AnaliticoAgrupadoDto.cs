namespace MagicEye3.Services.BackEndAPI.Models.Dto
{
    namespace MagicEye3.Services.BackEndAPI.Models.Dto
    {
        public class AnaliticoAgrupadoDto
        {
            public DateTime? laFecha { get; set; }

            /// <summary>
            /// Para la U. de Guayaquil, aquí almacenamos
            /// la suma de tiempos ACD y APE, pero en el “genérico”
            /// podríamos tenerlo en 0 (o ignorarlo) si no aplica.
            /// </summary>
            public int sumaTiemposACDyAPE { get; set; }

            /// <summary>
            /// Lista de todas las “filas” que comparten la misma fecha.
            /// </summary>
            public List<ReportAnaliticoDto2> Actividades { get; set; }
        }
    }
}
