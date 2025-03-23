using MagicEye3.ClienteMVC.Web.Models;

namespace MagicEye3.ClienteMVC.Web.Service.IService
{
    public interface IBaseService
    {

        Task<ResponseDto?> SendAsync(RequestDto requestDto, bool withBearer = true);
    }
}
