using CsvHelper.Configuration.Attributes;
using lestoma.CommonUtils.Enums;
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
        [Name("Nombre UPA")]
        public string NombreUpa { get; set; }
        [Name("Generado por")]
        public string Usuario { get; set; }
        [Name("Fecha de servidor")]
        public DateTime? FechaServidor { get; set; }
        [Name("Fecha de dispositivo")]
        public DateTime FechaDispositivo { get; set; }
        [Name("Componente")]
        public string Componente { get; set; }
        [Name("Módulo")]
        public string Modulo { get; set; }

        [Ignore]
        [Name("Trama de Entrada")]
        public string TramaIn { get; set; }

        [Ignore]
        [Name("Resultado Trama de Entrada")]
        public double? ResultTramaIn { get; set; }

        [Ignore]
        [Name("Trama de Salida")]
        public string TramaOut { get; set; }

        [Ignore]
        [Name("Resultado Trama de Salida")]
        public double? ResultTramaOut { get; set; }

        [Name("Estado Inicial")]
        public string ResultStatusInitial => GetStatusInitial(ResultTramaIn);
        [Name("Estado Final")]
        public string ResultStatusFinal => GetStatusFinal(ResultTramaOut, TramaOut);
        [Name("Set Point")]
        public string ResultSetPoint => GetSetPointOut(ResultTramaIn, TramaOut);
        [Name("Tipo de función")]
        public string Estado { get; set; }
        private string GetStatusInitial(double? resultTramaIn)
        {
            if (resultTramaIn.HasValue && Estado != EnumConfig.GetDescription(TipoEstadoComponente.Ajuste))
            {
                return resultTramaIn.Value switch
                {
                    0 => Constants.Constants.APAGADO,
                    1 => Constants.Constants.ENCENDIDO,
                    _ => "N/A",
                };
            }
            else
            {
                return "N/A";
            }
        }
        private string GetStatusFinal(double? resultTramaOut, string tramaOut)
        {
            if (tramaOut.Equals(Constants.Constants.TRAMA_SUCESS))
            {
                return HttpStatusCode.OK.ToString();
            }
            else if (resultTramaOut.HasValue)
            {
                return resultTramaOut.Value switch
                {
                    0 => Constants.Constants.APAGADO,
                    1 => Constants.Constants.ENCENDIDO,
                    _ => resultTramaOut.Value.ToString(),
                };
            }
            else { return "N/A"; }
        }
        private string GetSetPointOut(double? valorSetPointEnviado, string tramaRecibida)
        {
            string valor = "N/A";
            if (tramaRecibida.Equals(Constants.Constants.TRAMA_SUCESS))
            {
                valor = valorSetPointEnviado.ToString();
            }
            return valor;
        }
    }
}
