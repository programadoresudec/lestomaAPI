using System;
using System.Collections.Generic;
using System.Text;

namespace lestoma.CommonUtils.Requests
{
    public class TramaComponenteRequest
    {
        public Guid ComponenteId { get; set; }
        public string NombreComponente { get; set; }
        public List<byte> TramaOchoBytes { get; set; }
    }
}
