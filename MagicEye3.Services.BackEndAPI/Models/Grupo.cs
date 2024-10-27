using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MagicEye3.Services.BackEndAPI.Models
{
    public class Grupo
    {
        [Key]
        public int GrupoId {  get; set; }
        public string Nombre { get; set; }
        public ICollection<SilaboGrupo> SilaboGrupos { get; set; }

    }
}
