using lestoma.CommonUtils.DTOs;
using lestoma.Entidades.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace lestoma.Logica.Interfaces
{
    public interface IUpaService
    {
        Task<Response> CrearUpa(EUpa upa);
        Task<Response> EditarUpa(EUpa actividad);
        Task<List<EUpa>> ListaUpas();
        Task<Response> GetUpa(int id);
    }
}
