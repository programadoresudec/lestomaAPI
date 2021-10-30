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
        Task<Response> CrearAsync(T entidad);
        Task<Response> Merge(List<T> listadoEntidad);
        Task<Response> GetByIdAsync(TID id);
        Task<Response> ActualizarAsync(T entidad);
        Task EliminarAsync(TID id);
        Task<List<T>> GetAll();
    }
}
