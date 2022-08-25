using System;
using System.Collections.Generic;
using System.Text;

namespace lestoma.CommonUtils.DTOs
{
    public class ComponenteDTO : AuditoriaDTO
    {
        public Guid Id { get; set; }

        public string Nombre { get; set; }
    }
}
