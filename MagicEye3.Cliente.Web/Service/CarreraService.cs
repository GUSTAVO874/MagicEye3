using MagicEye3.Cliente.Web.Models;
using MagicEye3.Cliente.Web.Service.IService;
using MagicEye3.Cliente.Web.Utility;

namespace MagicEye3.Cliente.Web.Service
{
    public class CarreraService : ICarreraService
    {
        private readonly IBaseService _baseService;

        public CarreraService(IBaseService baseService)
        {
            _baseService = baseService;
        }

        public async Task<ResponseDto?> CreateCarreraAsync(CarreraDto carreraDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.POST,
                Data = carreraDto,
                Url = SD.BackEndAPIBase + "api/carrera/savecarrera"
            });
        }

        // Implementa otros métodos si es necesario
    }
}
