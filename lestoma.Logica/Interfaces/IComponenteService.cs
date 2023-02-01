using lestoma.CommonUtils.DTOs;
using lestoma.Entidades.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lestoma.Logica.Interfaces
{
    public interface IComponenteService : IGenericCRUD<EComponenteLaboratorio, Guid>
    {
        Task<IEnumerable<NameDTO>> GetComponentesJustNames();
        IQueryable<ListadoComponenteDTO> GetAllFilter(Guid upaId);
    }

}
