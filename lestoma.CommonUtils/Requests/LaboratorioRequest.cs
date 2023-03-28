using System;

namespace lestoma.CommonUtils.Requests
{
    public class LaboratorioRequest
    {
        public Guid Id { get; set; }
        public Guid ComponenteId { get; set; }
        public string TramaEnviada { get; set; }
        public string TramaRecibida { get; set; }
        public double? SetPointIn { get; set; }
        public double? SetPointOut { get; set; }
        public bool EstadoInternet { get; set; }
        public string Ip { get; set; }
        public string Session { get; set; }
        public string TipoDeAplicacion { get; set; }
        public DateTime FechaCreacionServer { get; set; }
        public DateTime? FechaCreacionDispositivo { get; set; }
    }
}
