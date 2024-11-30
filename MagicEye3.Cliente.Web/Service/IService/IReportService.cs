using MagicEye3.Cliente.Web.Models;

namespace MagicEye3.Cliente.Web.Service.IService
{
    public interface IReportService
    {
        Task<HttpResponseMessage> GenerateReportAsync(ReportDataDto data);
    }
}
