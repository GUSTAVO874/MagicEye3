using MagicEye3.Cliente.Web.Models;

namespace MagicEye3.Cliente.Web.Service.IService
{
    public interface IBaseService
    {

        Task<ResponseDto?> SendAsync(RequestDto requestDto, bool withBearer = true);
    }
}
