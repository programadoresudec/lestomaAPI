using lestoma.CommonUtils.Requests.Filters;
using System;
using System.Collections.Generic;
using System.Net;

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
        public double? ResultSetPointOut { get; set; }
        public string SetPointOut => GetSetPointOut(this.ResultSetPointOut);

        private string GetSetPointOut(double? valorCalculadoTramaRecibida)
        {
            string valor;
            switch (valorCalculadoTramaRecibida)
            {
                case 0:
                    valor = "Actuador Apagado.";
                    break;
                case 1:
                    valor = "Actuador Encendido.";
                    break;
                case 200:
                    valor = HttpStatusCode.OK.ToString();
                    break;
                case 409:
                    valor = $"{HttpStatusCode.Conflict} en la trama.";
                    break;
                case null:
                    valor = "N/A";
                    break;
                default:
                    valor = valorCalculadoTramaRecibida.ToString();
                    break;
            }
            return valor;
        }
    }
}
