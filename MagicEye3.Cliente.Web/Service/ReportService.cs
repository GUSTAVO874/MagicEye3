using MagicEye3.Cliente.Web.Models;
using MagicEye3.Cliente.Web.Service.IService;
using MagicEye3.Cliente.Web.Utility;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace MagicEye3.Cliente.Web.Service
{
    public class ReportService : IReportService
    {
        private readonly HttpClient _httpClient;
        private readonly ITokenProvider _tokenProvider;

        public ReportService(HttpClient httpClient, ITokenProvider tokenProvider)
        {
            _httpClient = httpClient;
            _tokenProvider = tokenProvider;
        }

        //funcionaba para generar pdf
        //public async Task<HttpResponseMessage> GenerateReportAsync(ReportDataDto data)
        //{
        //    var token = await _tokenProvider.GetTokenAsync();
        //    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        //    var response = await _httpClient.PostAsJsonAsync(SD.BackEndAPIBase + "api/report/generate", data);
        //    return response;
        //}

        //para dtos office
        public async Task<HttpResponseMessage> GenerateReportAsync(ReportDataDto data)
        {
            var token = await _tokenProvider.GetTokenAsync();
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var query = $"?format={data.Formato}";
            var response = await _httpClient.PostAsJsonAsync(SD.BackEndAPIBase + "api/report/generate" + query, data);
            //var response = await _httpClient.PostAsJsonAsync(SD.BackEndAPIBase + "api/report/GenerateReport3" + query, data);
            return response;
        }
        //public async Task<HttpResponseMessage> GenerateAnaliticAsync(ReportAnaliticoDto data)
        //{
        //    var token = await _tokenProvider.GetTokenAsync();
        //    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        //    var query = $"?format={data.Formato}";
        //    var response = await _httpClient.PostAsJsonAsync(SD.BackEndAPIBase + "api/report/generateanalitic" + query, data);
        //    return response;
        //}

    }
}
