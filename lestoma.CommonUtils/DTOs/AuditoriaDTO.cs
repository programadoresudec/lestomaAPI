using System;
using System.Collections.Generic;
using System.Text;

namespace lestoma.CommonUtils.DTOs
{
    public class AuditoriaDTO
    {
        public string Ip { get; set; }
        public string Session { get; set; }
        public string TipoDeAplicacion { get; set; }
        public DateTime FechaCreacionServer { get; set; }
        public DateTime? FechaActualizacionServer { get; set; }
    }
}
