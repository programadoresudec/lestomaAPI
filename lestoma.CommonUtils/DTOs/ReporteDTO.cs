using lestoma.CommonUtils.Requests.Filters;
using System;
using System.Collections.Generic;

namespace lestoma.CommonUtils.DTOs
{
    public class ReporteDTO
    {
        public string UserGenerator { get; set; }
        public List<ReportInfo> Reporte { get; set; }
        public DateFilterRequest FiltroFecha { get; set; }
    }

    public class ReportInfo
    {
        public string NombreUpa { get; set; }
        public string Usuario { get; set; }
        public DateTime? FechaServidor { get; set; }
        public DateTime FechaDispositivo { get; set; }
        public string Componente { get; set; }
        public string Modulo { get; set; }
        public string Estado { get; set; }
        public string SetPointIn { get; set; }
        public string SetPointOut { get; set; }
    }
}
