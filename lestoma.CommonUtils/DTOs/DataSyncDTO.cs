using lestoma.CommonUtils.Requests;
using System;
using System.Collections.Generic;
using System.Text;

namespace lestoma.CommonUtils.DTOs
{
    public class DataComponentSyncDTO : AuditoriaDTO
    {
        public ModuloDTO Modulo { get; set; }
        public Guid Id { get; set; }
        public Guid ActividadId { get; set; }
        public Guid UpaId { get; set; }
        public string NombreComponente { get; set; }
        public string DescripcionEstadoJson { get; set; }
    }
}
