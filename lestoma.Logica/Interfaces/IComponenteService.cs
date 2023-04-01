using lestoma.CommonUtils.DTOs;
using lestoma.CommonUtils.Requests.Filters;
using lestoma.Entidades.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lestoma.Logica.Interfaces
{
    public interface IComponenteService : IGenericCRUD<EComponenteLaboratorio, Guid>
    {
        Task<IEnumerable<NameDTO>> GetComponentsJustNames();
        IQueryable<ListadoComponenteDTO> GetAllFilter(UpaActivitiesFilterRequest upaActivitiesFilter);
        Task<IEnumerable<NameDTO>> GetComponentsJustNamesById(UpaActivitiesFilterRequest upaActivitiesfilter, bool IsAdmin);
        Task<ResponseDTO> UpdateByAdmin(EComponenteLaboratorio compDTO);
        Task<List<int>> GetRegistrationAddressesByUpaModulo(UpaModuleActivityFilterRequest FilterRequest);
    }

}
