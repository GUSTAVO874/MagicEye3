using MagicEye3.ClienteMVC.Web.Models;
using MagicEye3.ClienteMVC.Web.Service.IService;
using MagicEye3.ClienteMVC.Web.Utility;
using System.Threading.Tasks;

namespace MagicEye3.ClienteMVC.Web.Service
{
    
        public class CarreraPerCicParcService : ICarreraPerCicParcService
        {
            private readonly IBaseService _baseService;

            // Inyéctale el mismo BaseService que usas en Blazor 
            // o una versión adaptada a MVC con HttpClient
            public CarreraPerCicParcService(IBaseService baseService)
            {
                _baseService = baseService;
            }

            public async Task<ResponseDto?> CreateAsync(CarreraPerCicParcDto model)
            {
                return await _baseService.SendAsync(new RequestDto
                {
                    ApiType = SD.ApiType.POST,
                    Data = model,
                    Url = SD.BackEndAPIBase + "api/carrera/savecarrera2"
                    // Ajusta la ruta exacta de tu endpoint
                });
            }
        }
    
}
