namespace MagicEye3.Cliente.Web.Service.IService
{
    public interface ITokenProvider
    {
        Task SetTokenAsync(string token);
        Task<string> GetTokenAsync();
        Task ClearTokenAsync();
    }
}
