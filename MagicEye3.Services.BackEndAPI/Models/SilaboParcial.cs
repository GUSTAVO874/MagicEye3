namespace MagicEye3.Services.BackEndAPI.Models
{
    public class SilaboParcial
    {
        public int SilaboId { get; set; }
        public Silabo Silabo { get; set; }
        public int ParcialId { get; set; }
        public Parcial Parcial { get; set; }
    }
}
