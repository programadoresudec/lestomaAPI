using lestoma.CommonUtils.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lestoma.Logica.Interfaces
{
    public interface IGenericCRUD<T> where T : class
    {
        Task<Response> CrearAsync(T entidad);
        Task<Response> GetByIdAsync(object id);
        Task<Response> ActualizarAsync(T entidad);
        Task EliminarAsync(object id);
        Task<List<T>> GetAll();
    }
}
