namespace MagicEye3.Services.BackEndAPI.Models
{
    public class SilaboComponente
    {
        public int SilaboId { get; set; }
        
        public int ComponenteId { get; set; }
        public int HoraClase { get; set; } // cantidad en minutos que tiene la hora clase, actualmente 60 minutos
        public int Tiempo { get; set; } // tiempo en horas que tiene el componente

        public Componente Componente { get; set; }
        public Silabo Silabo { get; set; }

    }
}
