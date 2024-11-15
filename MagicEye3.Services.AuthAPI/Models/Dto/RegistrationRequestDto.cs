using System.ComponentModel.DataAnnotations;

namespace MagicEye3.Services.AuthAPI.Models.Dto
{
    public class RegistrationRequestDto
    {
        [Required(ErrorMessage = "El email es obligatorio")]
        [EmailAddress(ErrorMessage = "El email no es válido")]
        public string Email { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public string? Role { get; set; }
    }
}
