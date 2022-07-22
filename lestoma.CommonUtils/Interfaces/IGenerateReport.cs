using lestoma.CommonUtils.DTOs;
using lestoma.CommonUtils.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace lestoma.CommonUtils.Interfaces
{
    public interface IGenerateReport
    {
        public byte[] GeneratePdf(ReporteDTO reporte, bool IsSuperAdmin = false);
        public byte[] GenerateExcel(ReporteDTO reporte, bool IsSuperAdmin = false);
        public byte[] GenerateReportByFormat(GrupoTipoArchivo TipoFormato, ReporteDTO reporte, bool IsSuperAdmin = false);

    }
}
