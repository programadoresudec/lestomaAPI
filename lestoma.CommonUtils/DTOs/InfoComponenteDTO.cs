using lestoma.CommonUtils.Requests;
using System;
using System.Collections.Generic;
using System.Text;

namespace lestoma.CommonUtils.DTOs
{
    public class InfoComponenteDTO : NameDTO
    {
        public NameDTO Actividad { get; set; }
        public NameDTO Upa { get; set; }
        public NameDTO Modulo { get; set; }
        public EstadoComponenteDTO EstadoComponente { get; set; }
        public byte DireccionDeRegistro { get; set; }

    }
}
