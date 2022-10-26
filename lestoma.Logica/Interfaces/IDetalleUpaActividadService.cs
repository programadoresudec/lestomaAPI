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
        Task<IEnumerable<string>> GetActivities(int userId, Guid upaId);
        IQueryable<DetalleUpaActividadDTO> GetAllForPagination();
        Task<List<NameDTO>> GetActivitiesByUpaUserId(UpaUserFilterRequest filtro);
    }
}
