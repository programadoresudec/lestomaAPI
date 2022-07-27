using lestoma.CommonUtils.DTOs;
using lestoma.CommonUtils.Requests;
using System.Threading.Tasks;

namespace lestoma.Logica.Interfaces
{
    public interface IReporteService
    {
        Task<(byte[] ArchivoBytes, string MIME, string Archivo)> ReportByDate(FilterReportRequest filtro, bool isSuperAdmin);
        Task<(byte[] ArchivoBytes, string MIME, string Archivo)> ReportByComponents(FilterReportComponentRequest filtro, bool isSuperAdmin);
        Task<Response> DailyReport();
    }
}
