using System.Collections.Generic;
using System.Threading.Tasks;

namespace lestoma.Data
{
    public interface IGenericRepository<T> where T : class
    {
        Task Create(T entidad);
        Task<T> GetByIdAsync(object id);
        Task Update(T entidad);
        Task Delete(T entidad);
        Task<IEnumerable<T>> GetAll();
    }
}
