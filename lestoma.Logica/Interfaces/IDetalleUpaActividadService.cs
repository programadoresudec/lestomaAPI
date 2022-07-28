using lestoma.CommonUtils.DTOs;
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
        Task<Response> CreateInCascade(EUpaActividad entidad);
        Task<Response> UpdateInCascade(EUpaActividad entidad);
        Task<Guid> GetUpaByUserId(int userId);
        Task<IEnumerable<string>> GetActivities(int userId, Guid upaId);
        IQueryable<EUpaActividad> GetAllForPagination();
    }
}
