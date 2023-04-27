using lestoma.CommonUtils.DTOs;
using lestoma.CommonUtils.Requests;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace lestoma.Logica.Interfaces
{
    public interface IBuzonService
    {
        IQueryable<BuzonDTO> GetAllForPagination(Guid UpaId);
        Task<ResponseDTO> CreateMailBox(BuzonCreacionRequest buzonCreacion);
        Task<ResponseDTO> GetMailBoxById(int id);
    }
}
