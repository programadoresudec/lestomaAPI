using System;
using System.Collections.Generic;
using System.Text;

namespace lestoma.CommonUtils.DTOs
{
    public class ComponentesDTO : AuditoriaDTO
    {
        public Guid Id { get; set; }

        public string Nombre { get; set; }
    }
}
