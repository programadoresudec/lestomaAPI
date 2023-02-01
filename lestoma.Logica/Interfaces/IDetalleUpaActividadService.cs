using lestoma.CommonUtils.DTOs;
using lestoma.CommonUtils.Requests.Filters;
using lestoma.Entidades.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lestoma.Logica.Interfaces
{
    public interface IDetalleUpaActividadService
    {
        Task<IEnumerable<EUpaActividad>> GetAll();
        Task<ResponseDTO> CreateInCascade(EUpaActividad entidad);
        Task<ResponseDTO> UpdateInCascade(EUpaActividad entidad);
        Task<Guid> GetUpaByUserId(int userId);
        IQueryable<DetalleUpaActividadDTO> GetAllForPagination();
        Task<IEnumerable<NameDTO>> GetActivitiesByUpaUserId(UpaUserFilterRequest filtro);
        Task<IEnumerable<NameDTO>> GetActivitiesByUpaId(Guid idUpa);
    }
}
