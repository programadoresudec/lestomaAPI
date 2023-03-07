using System;
using System.Collections.Generic;
using lestoma.CommonUtils.Requests;

namespace lestoma.CommonUtils.DTOs
{
    public class UpaDTO : AuditoriaDTO
    {
        public Guid Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public short CantidadActividades { get; set; }
        public IEnumerable<ProtocoloRequest> ProtocolosCOM { get; set; }
    }
}
