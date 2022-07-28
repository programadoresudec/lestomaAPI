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
        Task<Response> Create(T entidad);
        Task<Response> GetById(TID id);
        Task<Response> Update(T entidad);
        Task Delete(TID id);
        Task<IEnumerable<T>> GetAll();
        IQueryable<T> GetAllForPagination();
    }
}
