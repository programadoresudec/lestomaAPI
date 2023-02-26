using lestoma.CommonUtils.DTOs;
using lestoma.CommonUtils.Requests.Filters;
using System.Threading.Tasks;

namespace lestoma.Logica.Interfaces
{
    public interface IReporteService
    {
        Task<(ReporteDTO reporte, ArchivoDTO archivo)> GetReportByDate(ReportFilterRequest filtro, bool isSuper);
        Task<(ReporteDTO reporte, ArchivoDTO archivo)> GetReportByComponents(ReportComponentFilterRequest filtro, bool isSuper);
        ArchivoDTO GenerateReportByDate(ReporteDTO reporte, ArchivoDTO archivo, ReportFilterRequest filtro, bool isSuper);
        ArchivoDTO GenerateReportByComponents(ReporteDTO reporte, ArchivoDTO archivo, ReportComponentFilterRequest filtro, bool isSuper);
        Task<ResponseDTO> GetDailyReport();
        Task SendReportByFilter(string email);
        Task<ResponseDTO> GenerateDailyReport(ReporteDTO reporte);
        Task<ResponseDTO> GetDailyReportTime();
    }
}
