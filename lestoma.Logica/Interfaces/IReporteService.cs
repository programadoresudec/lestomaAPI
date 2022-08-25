using lestoma.CommonUtils.DTOs;
using lestoma.CommonUtils.Requests.Filters;
using System.Threading.Tasks;

namespace lestoma.Logica.Interfaces
{
    public interface IReporteService
    {
        Task<(byte[] ArchivoBytes, string MIME, string Archivo)> ReportByDate(ReportFilterRequest filtro, bool isSuperAdmin);
        Task<(byte[] ArchivoBytes, string MIME, string Archivo)> ReportByComponents(ReportComponentFilterRequest filtro, bool isSuperAdmin);
        Task<Response> DailyReport();
    }
}
