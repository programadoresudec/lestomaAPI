using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using lestoma.Entidades.Models;
using lestoma.CommonUtils.DTOs;

namespace lestoma.Logica.Interfaces
{
    public interface IComponenteService : IGenericCRUD<EComponenteLaboratorio, Guid>
    {
        List<NameDTO> GetComponentesJustNames();
    }

}
