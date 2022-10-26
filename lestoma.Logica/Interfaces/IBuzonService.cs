using lestoma.CommonUtils.DTOs;
using lestoma.CommonUtils.Requests;
using lestoma.Entidades.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lestoma.Logica.Interfaces
{
    public interface IBuzonService
    {
        IQueryable<BuzonDTO> GetAllForPagination();
        Task<ResponseDTO> CreateMailBox(BuzonCreacionRequest buzonCreacion);
        Task<EBuzon> GetMailBoxById(int id);
    }
}
