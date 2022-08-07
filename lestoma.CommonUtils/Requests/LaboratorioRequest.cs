using lestoma.CommonUtils.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace lestoma.CommonUtils.Requests
{
    public class LaboratorioRequestOffline : AuditoriaDTO
    {
        public Guid Id { get; set; }
        public Guid ComponenteId { get; set; }
        public int TipoCOMId { get; set; }
        public string TramaEnviada { get; set; }
        public double SetPoint { get; set; }
        public bool EstadoInternet { get; set; }
        public DateTime FechaCreacionDispositivo { get; set; }
    }
}
