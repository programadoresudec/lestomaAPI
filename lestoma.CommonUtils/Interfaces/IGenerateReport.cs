using lestoma.CommonUtils.DTOs;
using lestoma.CommonUtils.Enums;
using System.Threading.Tasks;

namespace lestoma.CommonUtils.Interfaces
{
    public interface IGenerateReport
    {
        public byte[] GeneratePdf(ReporteDTO reporte, bool IsSuperAdmin);
        public byte[] GenerateExcel(ReporteDTO reporte);
        public Task<byte[]> GenerateCSV(ReporteDTO reporte);
        public Task<byte[]> GenerateReportByFormat(GrupoTipoArchivo TipoFormato, ReporteDTO reporte, bool IsSuperAdmin);

    }
}
