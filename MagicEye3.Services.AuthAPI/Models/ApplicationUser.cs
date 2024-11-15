using Microsoft.AspNetCore.Identity;

namespace MagicEye3.Services.AuthAPI.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Nombre { get; set; }
    }
}
