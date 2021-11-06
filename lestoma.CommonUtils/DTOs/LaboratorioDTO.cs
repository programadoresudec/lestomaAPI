using System;
using System.Collections.Generic;
using System.Text;

namespace lestoma.CommonUtils.DTOs
{
    public class LaboratorioDTO : AuditoriaDTO

    {
        public Guid Id { get; set; }
        public int ComponenteLaboratorioId { get; set; }
        public int TipoDeComunicacionId { get; set; }
        public int ActividadId { get; set; }
        public string TramaEnviada { get; set; }
    }
}