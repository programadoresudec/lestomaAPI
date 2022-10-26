using lestoma.CommonUtils.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lestoma.Logica.Interfaces
{
    public interface IGenericCRUD<T, TID> where T : class
    {
        Task<ResponseDTO> Create(T entidad);
        Task<ResponseDTO> GetById(TID id);
        Task<ResponseDTO> Update(T entidad);
        Task Delete(TID id);
        Task<IEnumerable<T>> GetAll();
        IQueryable<T> GetAllForPagination();
    }
}
