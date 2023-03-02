using Hangfire;
using lestoma.CommonUtils.DTOs;
using lestoma.CommonUtils.Requests.Filters;
using System.Threading.Tasks;

namespace lestoma.Logica.Interfaces
{
    public interface IReporteService
    {
        Task<(ReporteDTO reporte, ArchivoDTO archivo)> GetReportByDate(ReportFilterRequest filtro, string email);
        Task<(ReporteDTO reporte, ArchivoDTO archivo)> GetReportByComponents(ReportComponentFilterRequest filtro, string email);
        [AutomaticRetry(Attempts = 0)]
        Task<ArchivoDTO> GenerateReportByDate(ReportFilterRequest filtro, bool isSuper, string email);
        [AutomaticRetry(Attempts = 0)]
        Task<ArchivoDTO> GenerateReportByComponents(ReportComponentFilterRequest filtro, bool isSuper, string email);
        [AutomaticRetry(Attempts = 2)]
        Task<ResponseDTO> GetDailyReport();
        [AutomaticRetry(Attempts = 0)]
        Task SendReportByFilter(string email);
        Task<ResponseDTO> GenerateDailyReport(ReporteDTO reporte);
        Task<ResponseDTO> GetDailyReportTime();
    }
}
