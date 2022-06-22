using System.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lestoma.CommonUtils.DTOs;
using lestoma.Entidades.Models;
using System;
using System.Collections.Generic;

namespace lestoma.Logica.Interfaces
{
    public interface IModuloService : IGenericCRUD<EModuloComponente, int >
    {
      List<NameDTO> GetModuloJustNames();  
    }
} 
