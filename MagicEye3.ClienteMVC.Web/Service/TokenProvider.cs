using MagicEye3.ClienteMVC.Web.Service.IService;
using Blazored.LocalStorage;
//using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
//using Newtonsoft.Json.Linq;

namespace MagicEye3.ClienteMVC.Web.Service
{
    public class TokenProvider : ITokenProvider
    {
        private readonly ILocalStorageService _localStorage;
        private const string TokenKey = "authToken";

        public TokenProvider(ILocalStorageService localStorage)
        {
            _localStorage = localStorage;
        }

        public async Task SetTokenAsync(string token)
        {
            await _localStorage.SetItemAsync(TokenKey, token);
        }

        public async Task<string> GetTokenAsync()
        {
            return await _localStorage.GetItemAsync<string>(TokenKey);
        }

        public async Task ClearTokenAsync()
        {
            await _localStorage.RemoveItemAsync(TokenKey);
        }
    }
}
