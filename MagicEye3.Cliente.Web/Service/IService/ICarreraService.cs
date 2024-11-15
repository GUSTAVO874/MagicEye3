using MagicEye3.Cliente.Web.Models;

namespace MagicEye3.Cliente.Web.Service.IService
{
    public interface ICarreraService
    {
        Task<ResponseDto?> CreateCarreraAsync(CarreraDto carreraDto);
        // Puedes añadir más métodos si lo necesitas
    }
}

