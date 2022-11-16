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
        public string TramaRecibida { get; set; }  
        public double SetPointIn { get; set; }
        public double SetPointOut { get; set; }
        public bool EstadoInternet { get; set; }
        public DateTime FechaCreacionDispositivo { get; set; }
    }
}
