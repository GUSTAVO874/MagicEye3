using MagicEye3.Cliente.Web.Models;
using MagicEye3.Cliente.Web.Service.IService;
using Newtonsoft.Json;
using System.Net;
using System.Text;
using static MagicEye3.Cliente.Web.Utility.SD;

namespace MagicEye3.Cliente.Web.Service
{
    
    public class BaseService : IBaseService
    {
        private readonly HttpClient _httpClient;
        private readonly ITokenProvider _tokenProvider;

        public BaseService(HttpClient httpClient, ITokenProvider tokenProvider)
        {
            _httpClient = httpClient;
            _tokenProvider = tokenProvider;
        }

        public async Task<ResponseDto?> SendAsync(RequestDto requestDto, bool withBearer = true)
        {
            try
            {
                HttpRequestMessage message = new();
                message.Headers.Add("Accept", "application/json");

                // Agregar token si es necesario
                if (withBearer)
                {
                    var token = await _tokenProvider.GetTokenAsync();
                    if (!string.IsNullOrEmpty(token))
                    {
                        message.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                    }
                }

                // Establecer la URL de la solicitud
                //message.RequestUri = new Uri(requestDto.Url, UriKind.Relative);
                message.RequestUri = new Uri(requestDto.Url, UriKind.Absolute);


                // Establecer el contenido si es necesario
                if (requestDto.Data != null)
                {
                    message.Content = new StringContent(JsonConvert.SerializeObject(requestDto.Data), Encoding.UTF8, "application/json");
                }

                // Establecer el método HTTP
                message.Method = requestDto.ApiType switch
                {
                    ApiType.POST => HttpMethod.Post,
                    ApiType.PUT => HttpMethod.Put,
                    ApiType.DELETE => HttpMethod.Delete,
                    _ => HttpMethod.Get,
                };

                // Enviar la solicitud
                var apiResponse = await _httpClient.SendAsync(message);

                // Manejar la respuesta
                var apiContent = await apiResponse.Content.ReadAsStringAsync();
                var responseDto = JsonConvert.DeserializeObject<ResponseDto>(apiContent);

                if (!apiResponse.IsSuccessStatusCode)
                {
                    responseDto.IsSuccess = false;
                    responseDto.Message = apiContent;
                    return responseDto;
                }

                return responseDto;
            }
            catch (Exception ex)
            {
                return new ResponseDto
                {
                    Message = ex.Message,
                    IsSuccess = false
                };
            }
        }
    }

}
