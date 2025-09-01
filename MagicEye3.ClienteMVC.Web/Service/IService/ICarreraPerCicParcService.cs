using MagicEye3.ClienteMVC.Web.Models;

namespace MagicEye3.ClienteMVC.Web.Service.IService
{
    public interface ICarreraPerCicParcService
    {
        Task<ResponseDto?> CreateAsync(CarreraPerCicParcDto model);
    }
}
