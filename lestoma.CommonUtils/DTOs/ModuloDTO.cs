using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lestoma.CommonUtils.DTOs
{
    public class ModuloDTO : AuditoriaDTO
    {
       public Guid Id { get; set; }
       public string Nombre { get; set; }  
    }
}