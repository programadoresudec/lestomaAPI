using Hangfire;
using lestoma.CommonUtils.DTOs;
using lestoma.CommonUtils.Requests.Filters;
using System.Threading.Tasks;

namespace lestoma.Logica.Interfaces
{
    public interface IReporteService
    {
        Task<(ReporteDTO reporte, ArchivoDTO archivo)> GetReportByDate(ReportFilterRequest filtro, bool isSuper);
        Task<(ReporteDTO reporte, ArchivoDTO archivo)> GetReportByComponents(ReportComponentFilterRequest filtro, bool isSuper);
        [AutomaticRetry(Attempts = 2)]
        Task<ArchivoDTO> GenerateReportByDate(ReportFilterRequest filtro, bool isSuper);
        [AutomaticRetry(Attempts = 2)]
        Task<ArchivoDTO> GenerateReportByComponents(ReportComponentFilterRequest filtro, bool isSuper);
        Task<ResponseDTO> GetDailyReport();
        [AutomaticRetry(Attempts = 2)]
        Task SendReportByFilter(string email);
        Task<ResponseDTO> GenerateDailyReport(ReporteDTO reporte);
        Task<ResponseDTO> GetDailyReportTime();
    }
}
